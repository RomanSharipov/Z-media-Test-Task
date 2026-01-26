using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class PauseState : IState
    {
        private GameStateMachine _stateMachine;
        
        public void Initialize(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask Enter()
        {


            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {


            return UniTask.CompletedTask;
        }
    }
}