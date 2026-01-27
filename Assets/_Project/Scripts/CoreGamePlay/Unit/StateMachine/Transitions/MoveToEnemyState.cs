using UnityEngine;

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

            Vector3 direction = (_warrior.CurrentTarget.transform.position - _warrior.transform.position).normalized;
            _warrior.transform.position += direction * _warrior.Data.Speed * Time.deltaTime;
        }

        public override void Exit() { }
    }
}