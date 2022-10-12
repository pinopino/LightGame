using LightGame.GrainInterfaces;
using LightGame.Protocol;
using LightGame.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Orleans;
using System.Text;

namespace LightGame.API
{
    public class BaseController : Controller
    {
        const string dataqueryname = "data";
        const string tokenqueryname = "token";
        const string useridqueryname = "userid";
        protected string data = string.Empty;
        protected string token = string.Empty;
        protected long userId = 0;
        protected IClusterClient client;

        public BaseController(IClusterClient client)
        {
            this.client = client;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.HttpContext.Request.Query.ContainsKey(dataqueryname))
            {
                data = context.HttpContext.Request.Query[dataqueryname].ToString();
            }

            if (context.HttpContext.Request.Query.ContainsKey(tokenqueryname))
            {
                token = context.HttpContext.Request.Query[tokenqueryname].ToString();
            }

            if (context.HttpContext.Request.Query.ContainsKey(useridqueryname))
            {
                var struserid = context.HttpContext.Request.Query[useridqueryname].ToString();
                long.TryParse(struserid, out userId);
            }

            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var tokenGrain = client.GetGrain<ITokenGrain>(userId);
            var tokenInfo = tokenGrain.GetToken().Result;
            if (tokenInfo.Token != token ||
                tokenInfo.IP != ip ||
                tokenInfo.LastTime.AddSeconds(GameConsts.TokenExpire) < DateTime.Now)
            {
                var result = new LGResponse();
                result.ErrorCode = 10001;
                result.ErrorInfo = "Token Error";
                context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result), Encoding.UTF8);
                return;
            }
        }
    }
}
