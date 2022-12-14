using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using LightGame.Protocol;
using Microsoft.Extensions.Logging;

namespace LightGame.Silo
{
    public class SocketServerHandler : ChannelHandlerAdapter
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;


        public SocketServerHandler(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SocketServerHandler>();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            // links:
            // https://github.com/Azure/DotNetty/issues/265
            // https://www.cnblogs.com/kubixuesheng/p/12641418.html
            var revBuffer = message as IByteBuffer;
            if (revBuffer == null || revBuffer.ReadableBytes > ushort.MaxValue || revBuffer.ReadableBytes <= 2)
            {
                context.CloseAsync();
                return;
            }

            var dataBuffer = new byte[revBuffer.ReadableBytes];
            revBuffer.ReadBytes(dataBuffer);
            var msg = LGMsg.Parser.ParseFrom(dataBuffer);
            if (msg != null)
            {
                context.FireChannelRead(msg);
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception e)
        {
            _logger.LogError($"{nameof(SocketServerHandler)} {0}", e);
            context.CloseAsync();
        }
    }
}
