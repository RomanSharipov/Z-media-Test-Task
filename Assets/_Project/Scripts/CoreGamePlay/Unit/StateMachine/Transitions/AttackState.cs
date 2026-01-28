namespace CodeBase.CoreGamePlay
{
    public class AttackState : StateBase
    {
        public AttackState(Warrior warrior) : base(warrior) { }

        public override void Enter()
        {
            _warrior.Movement.Stop();
        }

        public override void UpdateState()
        {
            if (_warrior.CurrentTarget != null)
                _warrior.Movement.RotateToTarget(_warrior.CurrentTarget.transform.position);

            if (_warrior.CanAttack)
                _warrior.Attack();
        }

        public override void Exit() { }
    }
}