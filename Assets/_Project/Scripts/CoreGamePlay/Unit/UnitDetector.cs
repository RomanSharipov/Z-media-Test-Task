using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class UnitDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 10f;
        [SerializeField] private LayerMask _unitLayer;

        private Warrior _owner;
        private Collider[] _hitBuffer = new Collider[20];

        public void Initialize(Warrior owner)
        {
            _owner = owner;
        }

        public bool TryDetect(out Warrior target)
        {
            target = null;
            float closestDistance = float.MaxValue;

            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                _detectionRadius,
                _hitBuffer,
                _unitLayer
            );

            for (int i = 0; i < hitCount; i++)
            {
                if (!_hitBuffer[i].TryGetComponent(out Warrior warrior))
                    continue;
                
                if (warrior == _owner || !warrior.IsAlive)
                    continue;

                if (warrior.CurrentTeam == _owner.CurrentTeam)
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