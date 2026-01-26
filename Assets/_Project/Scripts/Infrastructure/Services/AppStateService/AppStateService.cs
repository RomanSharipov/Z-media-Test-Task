using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.Services
{
    public class AppStateService : IAppStateService
    {
        private GameStateMachine _mainGameStateMachine;

        public AppStateService(GameStateMachine mainGameStateMachine)
        {
            _mainGameStateMachine = mainGameStateMachine;
        }

        public void AddState(IState state)
        {
            _mainGameStateMachine.AddState(state.GetType(), state);
        }

        public async UniTask Enter<TState>() where TState : IState
        {
            await _mainGameStateMachine.Enter<TState>();
        }
    }
}
