using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class UnitDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 10f;

        private LayerMask _enemyLayer;
        private Warrior _owner;
        private Collider[] _hitBuffer = new Collider[20];

        public void Initialize(Warrior owner)
        {
            _owner = owner;

            if (_owner.CurrentTeam == TeamType.Player)
            {
                gameObject.layer = LayerMask.NameToLayer("PlayerUnit");
                _enemyLayer = LayerMask.GetMask("BotUnit");
            }
            else
            {
                gameObject.layer = LayerMask.NameToLayer("BotUnit");
                _enemyLayer = LayerMask.GetMask("PlayerUnit");
            }
        }

        public bool TryDetect(out Warrior target)
        {
            target = null;
            float closestDistance = float.MaxValue;

            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                _detectionRadius,
                _hitBuffer,
                _enemyLayer
            );

            for (int i = 0; i < hitCount; i++)
            {
                if (!_hitBuffer[i].TryGetComponent(out Warrior warrior))
                    continue;

                if (warrior == _owner || !warrior.IsAlive)
                    continue;

                float distance = Vector3.Distance(transform.position, warrior.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = warrior;
                }
            }

            return target != null;
        }
    }
}