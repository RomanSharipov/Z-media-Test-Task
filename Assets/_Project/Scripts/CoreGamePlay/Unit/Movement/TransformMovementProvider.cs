using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class TransformMovementProvider : IMovementProvider
    {
        private readonly Transform _transform;

        public TransformMovementProvider(Transform transform)
        {
            _transform = transform;
        }

        public void Move(Vector3 direction, float speed)
        {
            _transform.position += direction * speed * Time.deltaTime;
        }

        public void Stop() { }

        public void RotateTo(Vector3 direction, float rotationSpeed)
        {
            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}