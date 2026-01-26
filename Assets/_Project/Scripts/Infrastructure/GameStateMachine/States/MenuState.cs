using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;

namespace CodeBase.Infrastructure
{
    public class MenuState : IState
    {
        [Inject] private readonly IAssetProvider _assetProvider;
        [Inject] private readonly IAppStateService _appStateService;
        [Inject] private readonly IScreenSceneService _screenSceneService;

        private CompositeDisposable _compositeDisposable = new();

        [Inject]
        public MenuState()
        {

        }

        public UniTask Enter()
        {
            _screenSceneService.ShowPopup<IMainMenuScreen>(screen =>
            {
                screen.OnPlay.Subscribe(_ =>
                {
                    _appStateService.Enter<GameLoopState>();
                }).AddTo(_compositeDisposable);
            }).Forget();

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _screenSceneService.HidePopup<IMainMenuScreen>();

            
            _assetProvider.Cleanup();
            return UniTask.CompletedTask;
        }
    }
}
