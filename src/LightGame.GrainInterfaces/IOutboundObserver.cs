using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IOutboundObserver : IGrainObserver
    {
        Task SendPacket(LGMsg packet);
        Task Close(LGMsg packet = null);
    }
}
