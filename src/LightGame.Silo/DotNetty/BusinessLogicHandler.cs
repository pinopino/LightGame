using DotNetty.Transport.Channels;
using LightGame.Protocol;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orleans;

namespace LightGame.Silo
{
    public class BusinessLogicHandler : ChannelHandlerAdapter
    {
        private readonly ILogger _logger;
        private readonly IClusterClient _client;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private GameSession _session;

        public BusinessLogicHandler(IClusterClient client, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _client = client;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<BusinessLogicHandler>();
        }

        public override async void ChannelRead(IChannelHandlerContext context, object message)
        {
            var packet = message as LGMsg;
            if (packet == null)
            {
                await context.CloseAsync();
                return;
            }

            await _session.DispatchPacketAsync(packet);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception e)
        {
            _logger.LogError($"{nameof(BusinessLogicHandler)} {0}", e);
            context.CloseAsync();
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            _session = new GameSession(_client, _configuration, context, _loggerFactory);
            base.ChannelRegistered(context);
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            _session.Disconnect();
            base.ChannelUnregistered(context);
        }
    }
}
