namespace CodeBase.CoreGamePlay
{
    public class BattleStartedWithTargetTransition : TransitionBase
    {
        public BattleStartedWithTargetTransition(Warrior warrior, IState targetState) : base(warrior, targetState) { }

        public override bool ShouldTransition()
        {
            return _warrior.BattleStarted && _warrior.CurrentTarget != null && _warrior.CurrentTarget.IsAlive;
        }
    }
}