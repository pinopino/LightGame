using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IPacketRouterGrain : IGrainWithIntegerKey
    {
        Task SendPacket(LGMsg packet);
        Task Disconnect();
    }
}
