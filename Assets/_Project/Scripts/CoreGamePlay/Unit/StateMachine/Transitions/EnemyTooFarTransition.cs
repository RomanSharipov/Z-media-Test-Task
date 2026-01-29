namespace CodeBase.CoreGamePlay
{
    public class EnemyTooFarTransition : TransitionBase
    {
        public EnemyTooFarTransition(Warrior warrior, IState targetState) : base(warrior, targetState) { }

        public override bool ShouldTransition()
        {
            if (_warrior.CurrentTarget == null || !_warrior.CurrentTarget.IsAlive)
                return false;

            return !_warrior.AttackComponent.IsInAttackRange(_warrior.CurrentTarget);
        }
    }
}