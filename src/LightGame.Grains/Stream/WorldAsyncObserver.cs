using LightGame.Protocol;
using Orleans;
using Orleans.Streams;

namespace LightGame.Grains
{
    public class WorldAsyncObserver : BaseAsyncObserver
    {
        public WorldAsyncObserver(Grain grain, Func<LGMsg, StreamSequenceToken, Task> onNextAsync)
            : base(grain, onNextAsync)
        { }
    }
}
