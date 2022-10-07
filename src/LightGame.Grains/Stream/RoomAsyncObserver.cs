using LightGame.Protocol;
using Orleans;
using Orleans.Streams;

namespace LightGame.Grains
{
    public class RoomAsyncObserver : BaseAsyncObserver
    {
        public RoomAsyncObserver(Grain grain, Func<LGMsg, StreamSequenceToken, Task> onNextAsync)
            : base(grain, onNextAsync)
        { }
    }
}
