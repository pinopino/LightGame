using LightGame.Protocol;
using Orleans;

namespace LightGame.GrainInterfaces
{
    public interface IRoomFactoryGrain : IGrainWithIntegerKey
    {
        Task<int> CreateRoom(ProtoRoomInfo roomInfo);
        Task ReleaseRoom(int roomId);
        Task<ProtoRoomInfo> GetRoomInfo(int roomId);
        Task<bool> RoomExist(int roomId);
    }
}
