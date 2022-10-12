using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IRoomGrain : IGrainWithIntegerKey
    {
        Task Update();
        Task RoomNotify(LGMsg msg);

        Task Reconnect(IUserGrain user);
        Task PlayerEnterRoom(IUserGrain user);
        Task PlayerLeaveRoom(IUserGrain user);
        Task PlayerReady(IUserGrain user);
        Task PlayerSendMsg(IUserGrain user, string msg);
        Task PlayerCommand(IUserGrain user, List<CommandInfo> commands);
    }
}
