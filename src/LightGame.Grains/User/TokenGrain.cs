using LightGame.GrainInterfaces;
using Orleans;
using Orleans.Concurrency;

namespace LightGame.Grains
{
    [Reentrant]
    public class TokenGrain : Grain, ITokenGrain
    {
        private TokenInfo _tokenInfo;

        public Task SetToken(string token, string ip)
        {
            _tokenInfo = new TokenInfo();
            _tokenInfo.Token = token;
            _tokenInfo.IP = ip;
            _tokenInfo.LastTime = DateTime.Now;
            return Task.CompletedTask;
        }

        public Task<TokenInfo> GetToken()
        {
            return Task.FromResult(_tokenInfo);
        }

        public Task RefreshTokenTime()
        {
            _tokenInfo.LastTime = DateTime.Now;
            return Task.CompletedTask;
        }
    }
}
