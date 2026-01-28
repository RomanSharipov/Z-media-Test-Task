using CodeBase.Infrastructure;
using System;
using UniRx;
using UnityEngine;

namespace CodeBase.CoreGamePlay
{
    public class Warrior : MonoBehaviour
    {
        [SerializeField] private WarriorView _view;
        [SerializeField] private UnitDetector _unitDetector;
        [SerializeField] private WarriorAnimator _warriorAnimator;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private bool _battleStarted;
        [SerializeField] private Movement _movement;

        private float _attackCooldown;
        private WarriorStateMachine _stateMachine;

        private Subject<Unit> _onDied = new();

        private Health _health;
        public UnitData Data;

        public WarriorView View => _view;
        public WarriorAnimator Animator => _warriorAnimator;
        public UnitDetector UnitDetector => _unitDetector;
        public TeamType CurrentTeam { get; private set; }
        public Warrior CurrentTarget { get; set; }

        public Health Health => _health;
        public bool IsAlive => _health.IsAlive;
        public bool CanAttack => _attackCooldown <= 0f;


        public Movement Movement => _movement;
        public WarriorStateMachine StateMachine => _stateMachine;
        
        public IObservable<Unit> OnDied => _onDied;

        public bool BattleStarted => _battleStarted;

        public AttackComponent AttackComponent => _attackComponent;

        public void Initialize(UnitData data,TeamType team, WarriorAnimator warriorAnimator)
        {
            _warriorAnimator = warriorAnimator;
            Data = data;
            CurrentTeam = team;
            _health = new Health(data.HP);
            _movement.Initialize();

            _unitDetector.Initialize(this);
            _attackComponent.Initialize(this);

            _warriorAnimator.OnDamageFrame
                .Subscribe(_ => _attackComponent.Attack())
                .AddTo(this);

            _health.OnDied
                .Subscribe(_ => Die())
                .AddTo(this);
            InitializeStateMachine();
        }

        private void Update()
        {
            if (_attackCooldown > 0f)
                _attackCooldown -= Time.deltaTime;

            _stateMachine.Update();
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }

        public void Attack()
        {
            _warriorAnimator.PlayAttack();
            _attackCooldown = Data.AttackSpeed;
        }

        private void Die()
        {
            Destroy(gameObject);
            _onDied.OnNext(Unit.Default);
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new WarriorStateMachine();

            IState searchEnemyState = new SearchEnemyState(this);
            IState idleState = new IdleState(this);
            IState moveToEnemyState = new MoveToEnemyState(this);
            IState attackState = new AttackState(this);

            ITransition enemyDetected = new EnemyDetectedTransition(this, idleState);
            ITransition enemyInRange = new EnemyInAttackRangeTransition(this, attackState);
            ITransition targetLost = new TargetLostTransition(this, searchEnemyState);
            ITransition enemyTooFar = new EnemyTooFarTransition(this, moveToEnemyState);
            ITransition battleStartedWithTarget = new BattleStartedWithTargetTransition(this, moveToEnemyState);
            ITransition battleStarted = new BattleStartedTransition(this, searchEnemyState);

            searchEnemyState.AddTransitions(enemyInRange, enemyDetected);

            idleState.AddTransitions(enemyInRange, battleStartedWithTarget, battleStarted);

            moveToEnemyState.AddTransitions(targetLost, enemyInRange);

            attackState.AddTransitions(targetLost, enemyTooFar);

            _stateMachine.SetState(searchEnemyState);
        }

        public void StartBattle()
        {
            _battleStarted = true;
        }
    }
}