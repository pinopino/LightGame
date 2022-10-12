using LightGame.Common;
using LightGame.Entity;
using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using Orleans.Utilities;

namespace LightGame.Grains
{
    public class UserGrain : Grain, IUserGrain
    {
        private ILogger _logger;
        private GameUser _gameUser;
        private LGDataContext _dataContext;
        private BaseAsyncObserver _worldHandler;
        private BaseAsyncObserver _roomHandler;
        private ObserverManager<IOutboundObserver> _outboundSubsManager;

        public UserGrain(LGDataContext dataContext, ILogger<UserGrain> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
            _worldHandler = new WorldAsyncObserver(this, (msg, token) => { return Notify(msg); });
            _roomHandler = new RoomAsyncObserver(this, (msg, token) => { return Notify(msg); });
            _outboundSubsManager = new ObserverManager<IOutboundObserver>(TimeSpan.FromMinutes(5), logger);
        }

        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await base.OnActivateAsync(cancellationToken);
            _gameUser = _dataContext.GameUsers.First(m => m.UserId == this.GetPrimaryKeyLong());
        }

        public override async Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            _dataContext.Update(_gameUser);
            await base.OnDeactivateAsync(reason, cancellationToken);
        }

        #region Outbound Observe
        public Task Subscribe(IOutboundObserver observer)
        {
            _outboundSubsManager.Subscribe(observer, observer);

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IOutboundObserver observer)
        {
            _outboundSubsManager.Unsubscribe(observer);

            return Task.CompletedTask;
        }
        #endregion

        public Task SubscribeWorld(StreamId streamId)
        {
            return _worldHandler.Register(streamId);
        }

        public Task UnsubscribeWorld(StreamId streamId)
        {
            return _worldHandler.UnRegister(streamId);
        }

        public Task SubscribeRoom(StreamId streamId)
        {
            return _roomHandler.Register(streamId);
        }

        public Task UnsubscribeRoom(StreamId streamId)
        {
            return _roomHandler.UnRegister(streamId);
        }

        public Task Notify(LGMsg packet)
        {
            return _outboundSubsManager.Notify(s => s.SendPacket(packet));
        }

        public Task Kick()
        {
            return _outboundSubsManager.Notify(s => s.Close(new LGMsg { ErrorCode = (int)LGErrorType.Shown, ErrorInfo = "您的账号异地登录" }));
        }

        public Task SetNickName(string nickName)
        {
            _gameUser.NickName = nickName;
            _dataContext.Update(_gameUser);
            return Task.CompletedTask;
        }

        public Task<string> GetNickName()
        {
            return Task.FromResult(_gameUser.NickName);
        }
    }
}
