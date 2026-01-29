using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class WarriorView : MonoBehaviour
    {
        private Renderer _renderer;
        
        public void Initialize(Renderer renderer,Color color, float scale)
        {
            _renderer = renderer;

            SetColor(color);
            SetScale(scale);
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        public void SetScale(float scale)
        {
            transform.localScale = Vector3.one * scale;

            Vector3 position = transform.position;
            position.y = scale * 0.5f;
            transform.position = position;
        }
    }
}