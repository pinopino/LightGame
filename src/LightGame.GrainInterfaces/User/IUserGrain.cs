using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IUserGrain : IOutboundInterceptor
    {
        Task Notify(LGMsg packet);
        Task Kick();

        Task SubscribeGlobal(Guid streamId);
        Task UnsubscribeGlobal();

        Task SubscribeRoom(Guid streamId);
        Task UnsubscribeRoom();

        Task SetNickName(string nickName);
        Task<string> GetNickName();
    }
}
