namespace CodeBase.CoreGamePlay
{
    public class BattleStartedTransition : TransitionBase
    {
        public BattleStartedTransition(Warrior warrior, IState targetState) : base(warrior, targetState) { }

        public override bool ShouldTransition()
        {
            return _warrior.BattleStarted;
        }
    }
}