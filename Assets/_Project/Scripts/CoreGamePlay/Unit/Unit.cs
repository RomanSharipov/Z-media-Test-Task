using UnityEngine;
using UniRx;

namespace CodeBase.CoreGamePlay
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitView _view;

        public UnitData Data { get; private set; }
        public Health Health { get; private set; }
        public UnitView View => _view;

        public bool IsAlive => Health.IsAlive;

        public void Initialize(UnitData data, Color color, float scale)
        {
            Data = data;
            Health = new Health(data.HP);

            _view.SetColor(color);
            _view.SetScale(scale);

            Health.OnDamaged.Subscribe(_ => _view.PlayDamage()).AddTo(this);
            Health.OnDied.Subscribe(_ => OnDied()).AddTo(this);
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }

        private void OnDied()
        {
            _view.PlayDeath();
            Destroy(gameObject, 1f);
        }
    }
}

