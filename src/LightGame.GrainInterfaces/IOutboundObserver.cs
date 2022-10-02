using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IOutboundObserver : IGrainObserver
    {
        void SendPacket(LGMsg packet);
        void Close(LGMsg packet = null);
    }
}
