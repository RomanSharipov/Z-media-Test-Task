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
        [Inject] private readonly IScreenSceneService _screenSceneService;
        [Inject] private readonly ISceneObjectsProvider _sceneObjectsProvider;
        [Inject] private readonly IWarriorsOnLevel _warriorsOnLevel;

        [Inject] private readonly BattleState _battleState;
        [Inject] private readonly EmptyState _emptyState;
        [Inject] private readonly WinState _winState;
        [Inject] private readonly LoseState _loseState;


        private CompositeDisposable _compositeDisposable = new();

        public void Initialize()
        {
            RegisterChildState(_battleState);
            RegisterChildState(_emptyState);
            RegisterChildState(_loseState);
            RegisterChildState(_winState);

        }

        public override async UniTask Enter()
        {

            ISceneInitializer levelMain = await _levelService.LoadCurrentLevel();
            levelMain.InitializeSceneServices();

            _sceneObjectsProvider.WarriorsSpawner.RandomizeArmies();
            _screenSceneService.ShowPopup<IMainGameModeScreen>(screen =>
            {

                screen.OnMenu.Subscribe(_ =>
                {
                    _appStateService.Enter<MenuState>();
                }).AddTo(_compositeDisposable);

                screen.OnBattleButton.Subscribe(_ =>
                {
                    EnterChildState<BattleState>().Forget();
                }).AddTo(_compositeDisposable);

                screen.OnRandomizerButton.Subscribe(_ =>
                {
                    _sceneObjectsProvider.WarriorsSpawner.RandomizeArmies();
                }).AddTo(_compositeDisposable);
            }).Forget();

            _warriorsOnLevel.OnTeamDefeated.Subscribe(team =>
            {
                if (team == TeamType.Player)
                {
                    EnterChildState<LoseState>().Forget();
                }

                if (team == TeamType.Bot)
                {
                    EnterChildState<WinState>().Forget();
                }
            }).AddTo(_compositeDisposable);


            EnterChildState<EmptyState>().Forget();
        }
        
        protected override UniTask OnExit()
        {
            _screenSceneService.HidePopup<IMainGameModeScreen>();    
            _compositeDisposable.Clear();
            _warriorsOnLevel.ClearAll();
            _levelService.UnLoadCurrentLevel();
            _assetProvider.Cleanup();

            return UniTask.CompletedTask;
        }
    }
}