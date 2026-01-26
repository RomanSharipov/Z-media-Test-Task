using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        [Inject] private readonly IAppStateService _appStateService;

        public async UniTask Enter()
        {
            await _appStateService.Enter<MenuState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}
