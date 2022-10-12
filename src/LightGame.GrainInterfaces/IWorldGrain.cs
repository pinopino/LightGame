using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IWorldGrain : IGrainWithIntegerKey
    {
        Task Notify(LGMsg msg);
        Task<int> GetCount();
        Task PlayerEnterGlobalWorld(IUserGrain user);
        Task PlayerLeaveGlobalWorld(IUserGrain user);
    }
}
