using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private float _attackDistance = 1.5f;

        private Warrior _owner;

        public float AttackDistance => _attackDistance;

        public void Initialize(Warrior owner)
        {
            _owner = owner;
        }

        public bool IsInAttackRange(Warrior target)
        {
            if (target == null)
                return false;

            float distance = Vector3.Distance(transform.position, target.transform.position);
            return distance <= _attackDistance;
        }

        public void Attack()
        {
            if (_owner.CurrentTarget == null || !_owner.CurrentTarget.IsAlive)
                return;

            float damage = _owner.Data.Attack;
            _owner.CurrentTarget.TakeDamage(damage);
        }
    }
}