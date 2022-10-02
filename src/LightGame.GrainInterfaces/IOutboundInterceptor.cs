using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IOutboundInterceptor : IGrainWithIntegerKey
    {
        /// <summary>
        /// Subscribes to real-time notifications from this grain.
        /// </summary>
        /// <param name="observer">The observer to receive notifications.</param>
        Task Subscribe(IOutboundObserver observer);

        /// <summary>
        /// Unsubscribes the given viewer from real-time notifications from this grain.
        /// </summary>
        /// <param name="observer"></param>
        Task Unsubscribe(IOutboundObserver observer);
    }
}
