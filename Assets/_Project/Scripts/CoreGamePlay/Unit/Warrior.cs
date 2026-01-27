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
        private TeamType _currentTeam;
        public UnitView View => _unitView;

        public bool IsAlive => Health.IsAlive;
        
        private Subject<Unit> _onDied = new();


        public IObservable<Unit> Died => _onDied;

        public TeamType CurrentTeam => _currentTeam;

        public void Initialize(UnitData data, TeamType team)
        {
            _currentTeam = team;
            _unitDetector.Initialize(this);
            Data = data;
            Health = new Health(data.HP);
            
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

