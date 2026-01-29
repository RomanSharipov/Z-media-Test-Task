namespace CodeBase.CoreGamePlay
{
    public class MoveToEnemyState : StateBase
    {
        public MoveToEnemyState(Warrior warrior) : base(warrior) { }

        public override void Enter() { }

        public override void UpdateState()
        {
            if (_warrior.CurrentTarget == null)
                return;

            _warrior.Movement.MoveTo(_warrior.CurrentTarget.transform.position, _warrior.Data.Speed);
        }

        public override void Exit()
        {
            _warrior.Movement.Stop();
        }
    }
}