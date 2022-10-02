using LightGame.Common;
using LightGame.Entity;
using LightGame.GrainInterfaces;
using LightGame.Protocol;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using Orleans.Utilities;

namespace LightGame.Grains
{
    public class UserGrain : Grain, IUserGrain, IAsyncObserver<LGMsg>
    {
        private ILogger _logger;
        private GameUser _gameUser;
        private LGDataContext _dataContext;
        private StreamSubscriptionHandle<LGMsg> _globalHandler;
        private StreamSubscriptionHandle<LGMsg> _roomHandler;
        private ObserverManager<IOutboundObserver> _subsManager;

        public UserGrain(LGDataContext dataContext, ILogger<UserGrain> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
            _subsManager = new ObserverManager<IOutboundObserver>(TimeSpan.FromMinutes(5), logger);
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

        #region observe
        public Task Subscribe(IOutboundObserver observer)
        {
            _subsManager.Subscribe(observer, observer);

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IOutboundObserver observer)
        {
            _subsManager.Unsubscribe(observer);

            return Task.CompletedTask;
        }
        #endregion

        #region 订阅消息
        public Task OnCompletedAsync()
        {
            return Task.CompletedTask;
        }

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }

        public async Task OnNextAsync(LGMsg item, StreamSequenceToken token = null)
        {
            await Notify(item);
        }
        #endregion

        public Task Notify(LGMsg packet)
        {
            _subsManager.Notify(s => s.SendPacket(packet));

            return Task.CompletedTask;
        }

        public Task Kick()
        {
            _subsManager.Notify(s => s.Close(new LGMsg() { ErrorCode = (int)LGErrorType.Shown, ErrorInfo = "您的账号异地登录" }));

            return Task.CompletedTask;
        }

        public async Task SubscribeGlobal(Guid streamId)
        {
            if (_globalHandler == null)
            {
                //var streamProvider = this.GetStreamProvider(StreamProviders.JobsProvider);
                //var stream = streamProvider.GetStream<LGMsg>(streamId, StreamProviders.Namespaces.ChunkSender);
                //_globalHandler = await stream.SubscribeAsync(OnNextAsync);
            }
        }

        public async Task UnsubscribeGlobal()
        {
            //取消全服消息订阅
            if (_globalHandler != null)
            {
                await _globalHandler.UnsubscribeAsync();
                _globalHandler = null;
            }
        }

        public async Task SubscribeRoom(Guid streamId)
        {
            if (_roomHandler == null)
            {
                //var streamProvider = this.GetStreamProvider(StreamProviders.JobsProvider);
                //var stream = streamProvider.GetStream<LGMsg>(streamId, StreamProviders.Namespaces.ChunkSender);
                //_roomHandler = await stream.SubscribeAsync(OnNextAsync);
            }
        }

        public async Task UnsubscribeRoom()
        {
            if (_roomHandler != null)
            {
                await _roomHandler.UnsubscribeAsync();
                _roomHandler = null;
            }
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
