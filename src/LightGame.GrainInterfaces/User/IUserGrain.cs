using LightGame.Protocol;
using Orleans.Runtime;

namespace LightGame.GrainInterfaces
{
    public interface IUserGrain : IOutboundInterceptor
    {
        Task Notify(LGMsg packet);
        Task Kick();

        Task SubscribeWorld(StreamId streamId);
        Task UnsubscribeWorld(StreamId streamId);

        Task SubscribeRoom(StreamId streamId);
        Task UnsubscribeRoom(StreamId streamId);

        Task SetNickName(string nickName);
        Task<string> GetNickName();
    }
}
