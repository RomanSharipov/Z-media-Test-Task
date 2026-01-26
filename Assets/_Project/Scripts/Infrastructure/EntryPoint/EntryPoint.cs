using System.Threading;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class EntryPoint : IAsyncStartable
    {
        [Inject] private readonly IAppStateService _appStateService;

        
        [Inject] private readonly BootstrapState _bootstrapState;
        [Inject] private readonly MenuState _menuState;
        [Inject] private readonly GameLoopState _gameLoopState;
        
        public async UniTask StartAsync(CancellationToken cancellation = default)
        {
            
            _appStateService.AddState(_bootstrapState);
            _appStateService.AddState(_menuState);
            _appStateService.AddState(_gameLoopState);

            
            await _appStateService.Enter<BootstrapState>();
        }
    }
}
