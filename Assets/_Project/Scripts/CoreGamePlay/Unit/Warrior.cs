using System;
using UniRx;
using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class Warrior : MonoBehaviour
    {
        [SerializeField] private UnitDetector _unitDetector;
        [SerializeField] private UnitView _unitView;
        
        public UnitData Data { get; private set; }
        public Health Health { get; private set; }
        public UnitView View => _unitView;

        public bool IsAlive => Health.IsAlive;
        
        private Subject<Unit> _onDied = new();
        
        public IObservable<Unit> Died => _onDied;

        public TeamType CurrentTeam { get; internal set; }

        public void Initialize(UnitData data, Color color, float scale)
        {
            _unitDetector.Initialize(this);
            Data = data;
            Health = new Health(data.HP);

            View.SetColor(color);
            View.SetScale(scale);

            

            Health.OnDied.Subscribe(_ => 
            {
                _onDied.OnNext(Unit.Default);
            }).AddTo(this);
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }


    }
}

