using LightGame.Common;
using LightGame.GrainInterfaces;
using LightGame.Shared;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;

namespace LightGame.Grains
{
    public class UserIdFactoryGrain : Grain, IUserIdFactoryGrain
    {
        private readonly ILogger _logger;
        private readonly IPersistentState<long> _curUserId;

        public UserIdFactoryGrain(
            [PersistentState("CurUserId", StorageNameConsts.DefaultStorage)] IPersistentState<long> curUserId,
            ILogger<UserGrain> logger)
        {
            _curUserId = curUserId;
            _logger = logger;
        }

        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // The profile state will not be loaded at the time it is injected into the constructor,
            // so accessing it is invalid at that time. The state will be loaded before OnActivateAsync is called.
            base.OnActivateAsync(cancellationToken);
            return _curUserId.ReadStateAsync();
        }

        public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            _curUserId.WriteStateAsync();
            return base.OnDeactivateAsync(reason, cancellationToken);
        }

        public async Task<long> CreateUserId()
        {
            if (_curUserId.State == 0)
                _curUserId.State = GameConsts.UserSeed;
            
            _curUserId.State += RandomHelper.GetRandom(1, 1024);
            await _curUserId.WriteStateAsync();

            return _curUserId.State;
        }
    }
}
