using LightGame.Protocol;

namespace LightGame.GrainInterfaces
{
    public interface IUserGrain : IOutboundInterceptor
    {
        Task Notify(LGMsg packet);
        Task Kick();

        Task SubscribeWorld(Guid streamId);
        Task UnsubscribeWorld(Guid streamId);

        Task SubscribeRoom(Guid streamId);
        Task UnsubscribeRoom(Guid streamId);

        Task SetNickName(string nickName);
        Task<string> GetNickName();
    }
}
