using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class UnitDetector : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius = 10f;
        [SerializeField] private LayerMask _unitLayer;

        private Unit _owner;
        private Collider[] _hitBuffer = new Collider[20];

        public void Initialize(Unit owner)
        {
            _owner = owner;
        }

        public bool TryDetect(out Unit target)
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
                if (!_hitBuffer[i].TryGetComponent<Unit>(out var unit))
                    continue;

                if (unit == _owner || !unit.IsAlive)
                    continue;

                float distance = Vector3.Distance(transform.position, unit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = unit;
                }
            }

            return target != null;
        }
    }
}