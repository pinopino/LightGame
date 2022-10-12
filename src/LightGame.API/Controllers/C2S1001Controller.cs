using Google.Protobuf;
using LightGame.Common;
using LightGame.Protocol;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace LightGame.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class C2S1001Controller : BaseController
    {
        public C2S1001Controller(IClusterClient client)
            : base(client)
        { }

        [HttpGet]
        public string Get()
        {
            var param = C2S1001.Parser.ParseFrom(ByteString.FromBase64(data));

            string versionTotal = "1.0.0";
            string versionPatch = "1.0";

            switch ((LGDeviceType)param.MobileType)
            {
                case LGDeviceType.Android:
                    versionTotal = "1.0.0";
                    versionPatch = "1.0";
                    break;
                case LGDeviceType.Windows:
                    versionTotal = "1.0.0";
                    versionPatch = "1.0";
                    break;
                case LGDeviceType.iPhone:
                case LGDeviceType.iPad:
                case LGDeviceType.Mac:
                    versionTotal = "1.0.0";
                    versionPatch = "1.0";
                    break;
                case LGDeviceType.Unkown:
                    versionTotal = "1.0.0";
                    versionPatch = "1.0";
                    break;
            }

            S2C1001 message = new S2C1001();
            message.VersionTotal = versionTotal;
            message.VersionPatch = versionPatch;
            message.IsAppStorePass = false;//0是在审核,1是没审核
            message.FirUrl = "";
            message.ApkUrl = "";//不打开网页下载apk地址
            message.Doname = "";//热更新域名
            message.FixIp = "";//热更新IP

            return new LGResponse { Content = message.ToByteString() }.ToByteString().ToBase64();
        }
    }
}
