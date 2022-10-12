using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Orleans;

namespace LightGame.Grains
{
    public class RoomFactoryGrain : Grain, IRoomFactoryGrain
    {
        public async Task<int> CreateRoom(ProtoRoomInfo roomInfo)
        {
            throw new NotImplementedException();
        }

        public async Task ReleaseRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<ProtoRoomInfo> GetRoomInfo(int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RoomExist(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}