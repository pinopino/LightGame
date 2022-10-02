using Orleans;

namespace LightGame.GrainInterfaces
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public string IP { get; set; }
        public DateTime LastTime { get; set; }
    }

    public interface ITokenGrain : IGrainWithIntegerKey
    {
        Task SetToken(string token, string ip);
        Task<TokenInfo> GetToken();
        Task RefreshTokenTime();
    }
}
