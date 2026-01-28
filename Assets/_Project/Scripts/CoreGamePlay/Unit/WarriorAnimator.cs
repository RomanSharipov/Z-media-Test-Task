using System;
using UniRx;
using UnityEngine;


namespace CodeBase.CoreGamePlay
{
    public class WarriorAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;


        private readonly int AttackTrigger = Animator.StringToHash("Attack");
        
        private Subject<Unit> _onDamageFrame = new();

        public IObservable<Unit> OnDamageFrame => _onDamageFrame;


        public void PlayAttack()
        {
            _animator.SetTrigger(AttackTrigger);
        }
        
        // Animation Event
        public void DamageFrame()
        {
            _onDamageFrame.OnNext(Unit.Default);
        }
    }
}