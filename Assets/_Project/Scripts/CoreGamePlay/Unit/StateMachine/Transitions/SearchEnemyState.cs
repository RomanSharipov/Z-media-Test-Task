namespace CodeBase.CoreGamePlay
{
    public class SearchEnemyState : StateBase
    {
        public SearchEnemyState(Warrior warrior) : base(warrior) { }

        public override void Enter()
        {
            _warrior.CurrentTarget = null;
        }

        public override void UpdateState()
        {
            if (_warrior.UnitDetector.TryDetect(out var target))
                _warrior.CurrentTarget = target;
        }

        public override void Exit() { }
    }
}