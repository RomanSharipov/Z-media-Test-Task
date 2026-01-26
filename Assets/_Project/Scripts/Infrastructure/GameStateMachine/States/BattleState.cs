using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure
{
    /// <summary>
    /// BattleState - дочерний стейт GameLoopState, который сам является ParentState
    /// Имеет свои дочерние стейты: ReplaceWarriorsState, CombatState, VictoryState
    /// </summary>
    public class BattleState : ParentState
    {
        private GameStateMachine _parentStateMachine;


        public void Initialize(GameStateMachine parentStateMachine)
        {
            _parentStateMachine = parentStateMachine;
            //_combatState.Initialize(_childStateMachine);
            //_combatState2.Initialize(_childStateMachine);
            //RegisterChildState(_combatState);
            //RegisterChildState(_combatState2);

        }

        public override async UniTask Enter()
        {
            //_childStateMachine.Enter<CombatState>().Forget();
        }


        protected override UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }
    }
}