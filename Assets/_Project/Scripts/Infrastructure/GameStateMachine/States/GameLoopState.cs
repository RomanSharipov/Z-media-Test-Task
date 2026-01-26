using CodeBase.CoreGamePlay;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using UniRx;
using VContainer.Unity;

namespace CodeBase.Infrastructure
{
    public class GameLoopState : ParentState, IInitializable
    {
        [Inject] private readonly ILevelService _levelService;
        
        [Inject] private readonly IAssetProvider _assetProvider;
        [Inject] private readonly IAppStateService _appStateService;
        [Inject] private readonly IScreenSceneService _multiPopupService;

        [Inject] private readonly BattleState _battleState;
        [Inject] private readonly PauseState _pauseState;

        private CompositeDisposable _compositeDisposable = new();

        public void Initialize()
        {
            _battleState.Initialize(_childStateMachine);
            _pauseState.Initialize(_childStateMachine);
            
            RegisterChildState(_battleState);
            RegisterChildState(_pauseState);
        }

        public override async UniTask Enter()
        {
            ISceneInitializer levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();

            _multiPopupService.ShowPopup<IMainGameModeScreen>(screen =>
            {

                screen.OnMenu.Subscribe(_ =>
                {
                    _appStateService.Enter<MenuState>();
                }).AddTo(_compositeDisposable);

                screen.OnBattleButton.Subscribe(_ =>
                {
                    EnterChildState<BattleState>().Forget();
                }).AddTo(_compositeDisposable);

                screen.OnPauseButton.Subscribe(_ =>
                {
                    EnterChildState<PauseState>().Forget();
                }).AddTo(_compositeDisposable);


            }).Forget();
        
        }
        
        protected override UniTask OnExit()
        {
            _multiPopupService.HidePopup<IMainGameModeScreen>();    
            _compositeDisposable.Clear();
            
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();

            return UniTask.CompletedTask;
        }
    }
}