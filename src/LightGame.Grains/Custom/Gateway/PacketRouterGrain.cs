using Google.Protobuf;
using LightGame.Common;
using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Microsoft.Extensions.Logging;
using Orleans;

namespace LightGame.Grains
{
    public partial class PacketRouterGrain : Grain, IPacketRouterGrain
    {
        private IWorldGrain _world;
        private IRoomGrain _curRoom;
        private IUserGrain _user;
        private ILogger _logger;

        public PacketRouterGrain(ILogger<PacketRouterGrain> logger)
        {
            _logger = logger;
            _world = GrainFactory.GetGrain<IWorldGrain>(0);
        }

        public async Task SendPacket(LGMsg packet)
        {
            if (packet.ActionId == 100000)
            {
                //登录绑定
                _user = GrainFactory.GetGrain<IUserGrain>(packet.UserId);
                await _world.PlayerEnterGlobalWorld(_user);
                if (_curRoom != null)
                {
                    await _curRoom.Reconnect(_user);
                    //通知上线
                    var message = new LGMsg()
                    {
                        ActionId = 100,
                        Content =
                        new S2C100()
                        {
                            UserId = _user.GetPrimaryKeyLong(),
                            IsOnline = true
                        }.ToByteString()
                    };
                    await _curRoom.RoomNotify(message);
                }
                await _user.Notify(packet.ParseResult());
            }
            else
            {
                if (_user == null)
                {
                    await _user.Notify(packet.ParseResult(LGErrorType.Hidden, "用户未登录"));
                    return;
                }

                if (packet.ActionId == 100001)
                {
                    var req = C2S100001.Parser.ParseFrom(packet.Content);
                    if (_curRoom == null)
                    {
                        _curRoom = GrainFactory.GetGrain<IRoomGrain>(req.RoomId);
                    }
                    await _curRoom.PlayerEnterRoom(_user);
                }
                else
                {
                    if (_curRoom == null)
                    {
                        await _user.Notify(packet.ParseResult(LGErrorType.Hidden, "房间信息不存在"));
                        return;
                    }

                    switch (packet.ActionId)
                    {
                        case 100005:
                            {
                                await _curRoom.PlayerLeaveRoom(_user);
                                _curRoom = null;
                            }
                            break;
                        case 100007:
                            {
                                var req = C2S100007.Parser.ParseFrom(packet.Content);
                                await _curRoom.PlayerSendMsg(_user, req.Content);
                            }
                            break;
                        case 100009:
                            {
                                var req = C2S100009.Parser.ParseFrom(packet.Content);
                                await _curRoom.PlayerCommand(_user, req.Commands.ToList());
                            }
                            break;
                    }
                }
            }
        }

        public async Task Disconnect()
        {
            if (_user != null)
            {
                await _world.PlayerLeaveGlobalWorld(_user);
                if (_curRoom != null)
                {
                    //通知离线
                    var message = new LGMsg()
                    {
                        ActionId = 100,
                        Content =
                        new S2C100()
                        {
                            UserId = _user.GetPrimaryKeyLong(),
                            IsOnline = false
                        }.ToByteString()
                    };
                    await _curRoom.RoomNotify(message);
                }
            }
        }
    }
}
