using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class RigidbodyMovementProvider : IMovementProvider
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;

        public RigidbodyMovementProvider(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _transform = rigidbody.transform;
        }

        public void Move(Vector3 direction, float speed)
        {
            Vector3 velocity = direction * speed;
            velocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = velocity;
        }

        public void Stop()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.x = 0f;
            velocity.z = 0f;
            _rigidbody.velocity = velocity;
        }

        public void RotateTo(Vector3 direction, float rotationSpeed)
        {
            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rigidbody.MoveRotation(Quaternion.Slerp(_transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}