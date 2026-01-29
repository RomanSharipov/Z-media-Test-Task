using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Infrastructure
{

    public class LoseState : IState
    {
        [Inject] private IScreenSceneService _screenSceneService;
        [Inject] private IAppStateService _appStateService;

        private CompositeDisposable _compositeDisposable = new();

        public UniTask Enter()
        {
            _screenSceneService.ShowPopup<ILoseScreen>(screen =>
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
            _screenSceneService.HidePopup<ILoseScreen>();
            return UniTask.CompletedTask;

        }
    }
}