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
        [Inject] private readonly IScreenSceneService _multiPopupService;

        private CompositeDisposable _compositeDisposable = new();

        [Inject]
        public MenuState()
        {

        }

        public async UniTask Enter()
        {
            _multiPopupService.ShowPopup<IMainMenuScreen>(screen =>
            {
                screen.OnPlay.Subscribe(_ =>
                {
                    _appStateService.Enter<GameLoopState>();
                }).AddTo(_compositeDisposable);
            }).Forget();


            //_windowService.Open<MainMenu>(window =>
            //{
            //    window.OnStartGameButton.Subscribe(_ =>
            //    {
            //        _appStateService.Enter<GameLoopState>();
            //    }).AddTo(_compositeDisposable);
            //}).Forget();
        }

        public UniTask Exit()
        {
            _multiPopupService.HidePopup<IMainMenuScreen>();

            
            _assetProvider.Cleanup();
            return UniTask.CompletedTask;
        }
    }
}
