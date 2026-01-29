namespace CodeBase.CoreGamePlay
{
    public class EnemyDetectedTransition : TransitionBase
    {
        public EnemyDetectedTransition(Warrior warrior, IState targetState) : base(warrior, targetState) { }

        public override bool ShouldTransition()
        {
            return _warrior.CurrentTarget != null && _warrior.CurrentTarget.IsAlive;
        }
    }
}