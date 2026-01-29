using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure
{

    public class WinState : IState
    {
        [Inject] private IScreenSceneService _screenSceneService;
        [Inject] private IAppStateService _appStateService;

        private CompositeDisposable _compositeDisposable = new();

        public UniTask Enter()
        {
            _screenSceneService.ShowPopup<IWinScreen>(screen =>
            {
                screen.OnMenu.Subscribe(_ =>
                {
                    _appStateService.Enter<MenuState>();
                }).AddTo(_compositeDisposable);
            });

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _screenSceneService.HidePopup<IWinScreen>();


            return UniTask.CompletedTask;
            
        }
    }
}