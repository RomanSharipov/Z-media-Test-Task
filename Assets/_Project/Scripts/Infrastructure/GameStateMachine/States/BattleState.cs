using CodeBase.CoreGamePlay;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Infrastructure
{

    public class BattleState : IState
    {
        [Inject] private IWarriorsOnLevel _warriorsOnLevel;

        public UniTask Enter()
        {
            foreach (Warrior item in _warriorsOnLevel.BotWarriors)
            {
                item.StartBattle();
            }

            foreach (Warrior item in _warriorsOnLevel.PlayerWarriors)
            {
                item.StartBattle();

            }

            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
            
        }
    }
}