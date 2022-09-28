using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using LightGame.Protocol;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orleans;
using System.Diagnostics;
using System.Text;

using static DotNetty.Codecs.Http.HttpResponseStatus;
using static DotNetty.Codecs.Http.HttpVersion;

namespace LightGame.Silo
{
    public sealed class WebSocketServerHandler : SimpleChannelInboundHandler<object>
    {
        const string WebsocketPath = "/websocket";
        private readonly ILogger _logger;
        private readonly IClusterClient _client;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;

        private WebSocketServerHandshaker _handshaker;

        public WebSocketServerHandler(IClusterClient client, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _client = client;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<WebSocketServerHandler>();
        }

        protected override void ChannelRead0(IChannelHandlerContext context, object msg)
        {
            if (msg is IFullHttpRequest request)
            {
                this.HandleHttpRequest(context, request);
            }
            else if (msg is WebSocketFrame frame)
            {
                this.HandleWebSocketFrame(context, frame);
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        private void HandleHttpRequest(IChannelHandlerContext context, IFullHttpRequest req)
        {
            // Handle a bad request.
            if (!req.Result.IsSuccess)
            {
                SendHttpResponse(context, req, new DefaultFullHttpResponse(Http11, BadRequest));
                return;
            }

            // Allow only GET methods.
            if (!Equals(req.Method, DotNetty.Codecs.Http.HttpMethod.Get))
            {
                SendHttpResponse(context, req, new DefaultFullHttpResponse(Http11, Forbidden));
                return;
            }

            // Send notfound response for index page.
            if ("/".Equals(req.Uri))
            {
                var res = new DefaultFullHttpResponse(Http11, NotFound);
                SendHttpResponse(context, req, res);
                return;
            }

            // Handshake
            var wsFactory = new WebSocketServerHandshakerFactory(
                GetWebSocketLocation(req), null, true, 5 * 1024 * 1024);
            this._handshaker = wsFactory.NewHandshaker(req);
            if (this._handshaker == null)
            {
                WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(context.Channel);
            }
            else
            {
                this._handshaker.HandshakeAsync(context.Channel, req);
            }
        }

        private void HandleWebSocketFrame(IChannelHandlerContext context, WebSocketFrame frame)
        {
            // Check for closing frame
            if (frame is CloseWebSocketFrame)
            {
                _handshaker.CloseAsync(context.Channel, (CloseWebSocketFrame)frame.Retain());
                return;
            }

            if (frame is PingWebSocketFrame)
            {
                context.WriteAsync(new PongWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                return;
            }

            if (frame is TextWebSocketFrame)
            {
                var revBuffer = frame.Content as IByteBuffer;
                var dataBuffer = new byte[revBuffer.ReadableBytes];
                revBuffer.ReadBytes(dataBuffer);
                var msg = LGMsg.Parser.ParseFrom(ByteString.FromBase64(Encoding.UTF8.GetString(dataBuffer)));
                if (msg != null)
                {
                    //await _session.DispatchIncomingPacket(msg);
                }
                return;
            }

            if (frame is BinaryWebSocketFrame)
            {
                var revBuffer = frame.Content as IByteBuffer;
                var dataBuffer = new byte[revBuffer.ReadableBytes];
                revBuffer.ReadBytes(dataBuffer);
                var msg = LGMsg.Parser.ParseFrom(dataBuffer);
                if (msg != null)
                {
                    //await _session.DispatchIncomingPacket(msg);
                }
                return;
            }
        }

        static void SendHttpResponse(IChannelHandlerContext context, IFullHttpRequest req, IFullHttpResponse res)
        {
            // Generate an error page if response getStatus code is not OK (200).
            if (res.Status.Code != 200)
            {
                IByteBuffer buf = Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes(res.Status.ToString()));
                res.Content.WriteBytes(buf);
                buf.Release();
                HttpUtil.SetContentLength(res, res.Content.ReadableBytes);
            }

            // Send the response and close the connection if necessary.
            var task = context.Channel.WriteAndFlushAsync(res);
            if (!HttpUtil.IsKeepAlive(req) || res.Status.Code != 200)
            {
                task.ContinueWith((t, c) => ((IChannelHandlerContext)c).CloseAsync(),
                    context, TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception e)
        {
            _logger.LogError($"{nameof(WebSocketServerHandler)} {0}", e);
            context.CloseAsync();
        }

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            base.ChannelRegistered(context);
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            base.ChannelUnregistered(context);
        }

        private string GetWebSocketLocation(IFullHttpRequest req)
        {
            var result = req.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            Debug.Assert(result, "Host header does not exist.");
            var location = value.ToString() + WebsocketPath;

            if (SiloGateSetting.UseSsl)
            {
                return "wss://" + location;
            }
            else
            {
                return "ws://" + location;
            }
        }
    }
}
