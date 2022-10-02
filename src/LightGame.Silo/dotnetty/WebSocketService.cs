using DotNetty.Codecs.Http;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using DotNetty.Transport.Libuv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace LightGame.Silo
{
    public class WebSocketService : IHostedService
    {
        private readonly IClusterClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        private IEventLoopGroup _bossGroup;
        private IEventLoopGroup _workGroup;
        private IEventLoopGroup _businessGroup;
        private IChannel _bootstrapChannel;

        public WebSocketService(IClusterClient client, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _client = client;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<WebSocketService>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
#if !DEBUG
            ResourceLeakDetector.Level = ResourceLeakDetector.DetectionLevel.Disabled;
#endif
            _logger.LogInformation(
                $"{Environment.NewLine}{RuntimeInformation.OSArchitecture} {RuntimeInformation.OSDescription}"
                + $"{Environment.NewLine}{RuntimeInformation.ProcessArchitecture} {RuntimeInformation.FrameworkDescription}"
                + $"{Environment.NewLine}Processor Count : {Environment.ProcessorCount}{Environment.NewLine}");

            var useLibuv = SiloGateSetting.UseLibuv;
            _logger.LogInformation("Transport type : " + (useLibuv ? "Libuv" : "Socket"));

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
            }

            _logger.LogInformation($"Server garbage collection : {(GCSettings.IsServerGC ? "Enabled" : "Disabled")}{Environment.NewLine}");
            _logger.LogInformation($"Current latency mode for garbage collection: {GCSettings.LatencyMode}{Environment.NewLine}");

            if (useLibuv)
            {
                var dispatcher = new DispatcherEventLoopGroup();
                _bossGroup = dispatcher;
                _workGroup = new WorkerEventLoopGroup(dispatcher);
            }
            else
            {
                _bossGroup = new MultithreadEventLoopGroup(1);
                _workGroup = new MultithreadEventLoopGroup();
            }
            _businessGroup = new MultithreadEventLoopGroup();

            X509Certificate2 tlsCertificate = null;
            var useSsl = SiloGateSetting.UseSsl;
            if (useSsl)
            {
                var processDirectory = string.Empty;
#if NETSTANDARD2_0 || NETCOREAPP3_1_OR_GREATER || NET5_0_OR_GREATER
                processDirectory = AppContext.BaseDirectory;
#else
                processDirectory = AppDomain.CurrentDomain.BaseDirectory;
#endif
                var pfx = SiloGateSetting.pfxFile;
                var pwd = SiloGateSetting.pfxPwd;
                tlsCertificate = new X509Certificate2(Path.Combine(processDirectory, pfx), pwd);
            }

            var bootstrap = new ServerBootstrap();
            bootstrap.Group(_bossGroup, _workGroup);
            if (useLibuv)
            {
                bootstrap.Channel<TcpServerChannel>();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                    || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    bootstrap
                        .Option(ChannelOption.SoReuseport, true)
                        .ChildOption(ChannelOption.SoReuseaddr, true);
                }
            }
            else
            {
                bootstrap.Channel<TcpServerSocketChannel>();
            }

            bootstrap
                .Option(ChannelOption.SoBacklog, 8192)
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    if (tlsCertificate != null)
                    {
                        pipeline.AddLast(TlsHandler.Server(tlsCertificate));
                    }

                    pipeline.AddLast(new HttpServerCodec());
                    pipeline.AddLast(new HttpObjectAggregator(65536));
                    pipeline.AddLast(new WebSocketServerHandler(_loggerFactory));
                    pipeline.AddLast(_businessGroup, new BusinessLogicHandler(_client, _configuration, _loggerFactory));
                }));

            var ip = SiloGateSetting.IP;
            var port = SiloGateSetting.Port;
            _bootstrapChannel = await bootstrap.BindAsync(ip, port);

            _logger.LogInformation("Websocket service started, listening on "
                + $"{(useSsl ? "wss" : "ws")}"
                + $"://{ip}:{port}/websocket" + Environment.NewLine);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _bootstrapChannel.CloseAsync();
                _logger.LogInformation("Websocket service stoped.");
            }
            finally
            {
                await _bossGroup.ShutdownGracefullyAsync();
                await _workGroup.ShutdownGracefullyAsync();
            }
        }
    }
}
