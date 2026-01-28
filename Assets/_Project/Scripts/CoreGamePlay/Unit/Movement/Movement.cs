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
            _provider = new TransformMovementProvider(transform);
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


    }
}