using Google.Protobuf;
using LightGame.Common;
using LightGame.Protocol;

namespace LightGame.Grains
{
    public static class PacketExtension
    {
        public static LGMsg ParseResult(this LGMsg packet, LGErrorType errorType = LGErrorType.Success, string errorInfo = "")
        {
            packet.Token = string.Empty;
            packet.Content = ByteString.Empty;
            packet.ErrorCode = (int)errorType;
            packet.ErrorInfo = errorInfo;

            return packet;
        }

        public static bool ValidateSign(this LGMsg packet, string secretKey)
        {
            var sign = packet.Sign;
            packet.Sign = string.Empty;

            return $"{packet.ToByteString()}{secretKey}".ToMd5String() != sign;
        }
    }
}
