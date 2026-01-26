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

        public override UniTask Enter()
        {
            //_childStateMachine.Enter<CombatState>().Forget();
            return UniTask.CompletedTask;
        }


        protected override UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }
    }
}