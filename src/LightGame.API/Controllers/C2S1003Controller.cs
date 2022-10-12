using Google.Protobuf;
using LightGame.Common;
using LightGame.Entity;
using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace LightGame.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class C2S1003Controller : BaseController
    {
        private readonly ILogger _logger;
        private readonly LGDataContext _dataContext;
        private readonly LGRecordContext _recordContext;
        private readonly IClusterClient _client;

        public C2S1003Controller(IClusterClient client, LGDataContext dataContext, LGRecordContext recordContext, ILogger<C2S1003Controller> logger)
            : base(client)
        {
            _client = client;
            _dataContext = dataContext;
            _recordContext = recordContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var req1003 = C2S1003.Parser.ParseFrom(ByteString.FromBase64(data));

            var user = _dataContext.GameUsers.Where(m => m.DeviceId == req1003.DeviceId).FirstOrDefault();
            long userId = 0;
            if (user == null || user.UserId == 0)
            {
                //注册账号
                var userIdFactory = _client.GetGrain<IUserIdFactoryGrain>(default(int));
                userId = await userIdFactory.CreateUserId();
                user = new GameUser();
                user.UserId = userId;
                user.NickName = "游客001";
                user.HeadIcon = "1";
                user.DeviceId = req1003.DeviceId;
                _dataContext.Add(user);
                _dataContext.SaveChanges();
            }
            else
            {
                userId = user.UserId;
            }

            //将玩家踢出游戏
            var userGrain = _client.GetGrain<IUserGrain>(userId);
            await userGrain.SetNickName(req1003.DeviceId);
            await userGrain.Kick();
            var token = $"{userId}{Guid.NewGuid()}{DateTime.UtcNow.Ticks}".ToMd5String();
            var tokenGtain = _client.GetGrain<ITokenGrain>(userId);
            tokenGtain.SetToken(token, HttpContext.Connection.RemoteIpAddress.ToString()).Wait();

            _recordContext.Add(new LoginRecord
            {
                UserId = userId,
                LoginType = LoginType.None,
                LoginIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                LoginDevice = req1003.DeviceId
            });
            await _recordContext.SaveChangesAsync();

            var serverconfig = _dataContext.ServerConfigs.Where(m => m.ServerLevel == 0).FirstOrDefault();
            if (serverconfig == null)
            {
                return new LGResponse { ErrorCode = (int)LGErrorType.Shown, ErrorInfo = "服务器不存在" }.ToByteString().ToBase64();
            }

            var message = new S2C1003();
            message.Token = token;
            message.UserId = userId;
            message.UserName = req1003.DeviceId;
            message.LoginIP = serverconfig.LoginIP;
            message.LoginPort = serverconfig.LoginPort;
            message.ApiIP = serverconfig.ApiIP;
            message.ApiPort = serverconfig.ApiPort;

            return new LGResponse { Content = message.ToByteString() }.ToByteString().ToBase64();
        }
    }
}
