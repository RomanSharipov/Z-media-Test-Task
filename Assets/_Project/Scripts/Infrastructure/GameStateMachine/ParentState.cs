using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure
{
    /// <summary>
    /// Базовый класс для стейтов, которые могут содержать дочерние стейты
    /// </summary>
    public abstract class ParentState : IState
    {
        protected readonly GameStateMachine _childStateMachine;

        protected ParentState()
        {
            _childStateMachine = new GameStateMachine();
        }


        public GameStateMachine GetChildStateMachine()
        {
            return _childStateMachine;
        }


        protected void RegisterChildState<TState>(TState state) where TState : IState
        {
            _childStateMachine.AddState(typeof(TState), state);
        }


        protected async UniTask EnterChildState<TChildState>() where TChildState : IState
        {
            await _childStateMachine.Enter<TChildState>();
        }
        
        public IState GetActiveChildState()
        {
            return _childStateMachine.ActiveState;
        }

        public abstract UniTask Enter();
        
        public async UniTask Exit()
        {
            await _childStateMachine.ExitActiveState();
            await OnExit();
        }
        
        protected virtual UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }
    }
}