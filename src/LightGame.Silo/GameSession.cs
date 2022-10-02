using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using LightGame.Common;
using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;

namespace LightGame.Silo
{
    public class GameSession
    {
        class OutboundObserver : IOutboundObserver
        {
            private readonly GameSession session;

            public OutboundObserver(GameSession session)
            {
                this.session = session;
            }

            public async void Close(LGMsg packet = null)
            {
                if (packet != null)
                    await session.SendAsync(packet);
                await session.Close();
            }

            public async void SendPacket(LGMsg packet)
            {
                await session.SendAsync(packet);
            }
        }

        private readonly IClusterClient _client;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        private IChannelHandlerContext _context;
        private IPacketRouterGrain _router;
        private bool _isInit;
        private string _secretKey;

        public GameSession(IClusterClient client, IConfiguration configuration, IChannelHandlerContext context, ILoggerFactory loggerFactory)
        {
            _client = client;
            _configuration = configuration;
            _context = context;
            _logger = loggerFactory.CreateLogger<GameSession>();
            _secretKey = _configuration.GetValue<string>("SecretKey");
        }

        public async Task DispatchPacketAsync(LGMsg packet)
        {
            try
            {
                // 验证sign
                if (!packet.ValidateSign(_secretKey))
                {
                    await SendAsync(packet.ParseResult(LGErrorType.Hidden, "签名验证失败"));
                    await Close();
                    return;
                }

                // 验证token
                var userId = packet.UserId;
                var tokenGrain = _client.GetGrain<ITokenGrain>(userId);
                var tokenInfo = await tokenGrain.GetToken();
                if (tokenInfo == null || tokenInfo.Validate(packet.Token))
                {
                    await SendAsync(packet.ParseResult(LGErrorType.Hidden, "Token验证失败"));
                    await Close();
                    return;
                }

                //同步初始化
                if (!_isInit)
                {
                    var userGrain = _client.GetGrain<IUserGrain>(userId);
                    var outboundObserver = new OutboundObserver(this);
                    var outboundObserverRef = _client.CreateObjectReference<IOutboundObserver>(outboundObserver);
                    await userGrain.Subscribe(outboundObserverRef);

                    _router = _client.GetGrain<IPacketRouterGrain>(userId);
                    _isInit = true;
                }

                //心跳包
                if (packet.ActionId == 1)
                {
                    await tokenGrain.RefreshTokenTime();
                    await SendAsync(packet.ParseResult());
                    return;
                }

                await _router.SendPacket(packet);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"DispatchIncomingPacket异常:\n" +
                    $"{ex.Message}\n" +
                    $"{ex.StackTrace}\n" +
                    $"{JsonConvert.SerializeObject(packet)}");
            }
        }

        public async Task SendAsync(LGMsg packet)
        {
            try
            {
                if (_context.Channel.Active)
                {
                    var bytes = packet.ToByteArray();
                    IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
                    await _context.WriteAndFlushAsync(buffer);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"SendAsync异常:\n" +
                    $"{ex.Message}\n" +
                    $"{ex.StackTrace}\n" +
                    $"{JsonConvert.SerializeObject(packet)}");
            }
        }

        public void Disconnect()
        {
            if (_router != null)
                _router.Disconnect();
        }

        public async Task Close()
        {
            await _context.CloseAsync();
        }
    }
}
