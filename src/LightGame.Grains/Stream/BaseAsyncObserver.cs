using LightGame.Protocol;
using Orleans;
using Orleans.Runtime;
using Orleans.Streams;

namespace LightGame.Grains
{
    public class BaseAsyncObserver : IAsyncObserver<LGMsg>
    {
        private Grain _grain;
        private Func<LGMsg, StreamSequenceToken, Task> _onNextAsync;

        public BaseAsyncObserver(Grain grain, Func<LGMsg, StreamSequenceToken, Task> onNextAsync)
        {
            _onNextAsync = onNextAsync;
            _grain = grain;
        }

        protected virtual string StreamProviderName => "DefaultStreamProvider";

        public virtual async Task Register(StreamId streamId)
        {
            var streamProvider = _grain.GetStreamProvider(StreamProviderName);
            var stream = streamProvider.GetStream<LGMsg>(streamId);

            var subscriptionHandles = await stream.GetAllSubscriptionHandles();
            if (subscriptionHandles == null || subscriptionHandles.Count == 0)
            {
                await stream.SubscribeAsync(OnNextAsync);
            }
            else
            {
                foreach (var handle in subscriptionHandles)
                {
                    await handle.ResumeAsync(OnNextAsync);
                }
            }
        }

        public virtual async Task UnRegister(StreamId streamId)
        {
            var streamProvider = _grain.GetStreamProvider(StreamProviderName);
            var stream = streamProvider.GetStream<LGMsg>(streamId);

            var subscriptionHandles = await stream.GetAllSubscriptionHandles();
            if (subscriptionHandles != null)
            {
                foreach (var handle in subscriptionHandles)
                {
                    await handle.UnsubscribeAsync();
                }
            }
        }

        public virtual Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }

        public virtual Task OnNextAsync(LGMsg item, StreamSequenceToken token = null)
        {
            return _onNextAsync(item, token);
        }
    }
}
