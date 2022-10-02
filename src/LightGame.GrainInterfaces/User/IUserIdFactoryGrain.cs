using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IUserIdFactoryGrain : IGrainWithIntegerKey
    {
        Task<long> CreateUserId();
    }
}
