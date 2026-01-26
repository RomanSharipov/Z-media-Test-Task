using System;
using UniRx;

 

public class Health
{
    private readonly float _maxHP;
    private float _currentHP;

    
    private Subject<float> _onDamaged = new();
    private Subject<Unit> _onDied = new();

    public IObservable<float> OnDamaged => _onDamaged;
    public IObservable<Unit> OnDied => _onDied;

    public float Current => _currentHP;
    public float Max => _maxHP;
    public bool IsAlive => _currentHP > 0;

    public Health(float maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (!IsAlive) return;

        _currentHP -= damage;
        _onDamaged.OnNext(damage);

        if (_currentHP <= 0)
        {
            _currentHP = 0;
            _onDied.OnNext(Unit.Default);
        }
    }
}