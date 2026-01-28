using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10f;

        private IMovementProvider _provider;
        private Vector3 _currentDirection;
        private float _currentSpeed;
        private bool _isMoving;

        public void Initialize()
        {
            _provider = new RigidbodyMovementProvider(GetComponent<Rigidbody>());
        }

        public void MoveTo(Vector3 targetPosition, float speed)
        {
            _currentDirection = (targetPosition - transform.position).normalized;
            _currentDirection.y = 0f;
            _currentSpeed = speed;
            _isMoving = true;
        }

        public void Stop()
        {
            _isMoving = false;
            _provider.Stop();
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            
            _provider.Move(_currentDirection, _currentSpeed);
            _provider.RotateTo(_currentDirection, _rotationSpeed);
        }

        public void RotateToTarget(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0f;

            if (direction == Vector3.zero)
                return;

            _provider.RotateTo(direction, _rotationSpeed);
        }
    }
}