using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public interface IMovementProvider
    {
        public void Move(Vector3 direction, float speed);
        public void Stop();
        public void RotateTo(Vector3 direction, float rotationSpeed);
    }
}