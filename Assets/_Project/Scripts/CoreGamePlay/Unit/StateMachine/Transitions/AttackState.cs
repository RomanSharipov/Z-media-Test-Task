namespace CodeBase.CoreGamePlay
{
    public class AttackState : StateBase
    {
        public AttackState(Warrior warrior) : base(warrior) { }

        public override void Enter() { }

        public override void UpdateState()
        {
            if (_warrior.CanAttack)
                _warrior.StartAttack();
        }

        public override void Exit() { }
    }
}