using UnityEngine;
using UniRx;

namespace CodeBase.CoreGamePlay
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitDetector _unitDetector;


        public UnitData Data { get; private set; }
        public Health Health { get; private set; }
        public UnitView View { get; private set; }

        public bool IsAlive => Health.IsAlive;

        public void Initialize(UnitData data, Color color, float scale, UnitView unitView)
        {
            View = unitView;
            _unitDetector.Initialize(this);
            Data = data;
            Health = new Health(data.HP);

            View.SetColor(color);
            View.SetScale(scale);

            Health.OnDamaged.Subscribe(_ => View.PlayDamage()).AddTo(this);
            Health.OnDied.Subscribe(_ => OnDied()).AddTo(this);
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }

        private void OnDied()
        {
            View.PlayDeath();
            Destroy(gameObject, 1f);
        }
    }
}

