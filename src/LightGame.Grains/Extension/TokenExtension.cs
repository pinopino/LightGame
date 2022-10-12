using LightGame.GrainInterfaces;
using LightGame.Shared;

namespace LightGame.Grains
{
    public static class TokenExtension
    {
        public static bool Validate(this TokenInfo tokenInfo, string comparand)
        {
            return tokenInfo.Token != comparand || tokenInfo.LastTime.AddSeconds(GameConsts.TokenExpire) < DateTime.Now;
        }
    }
}
