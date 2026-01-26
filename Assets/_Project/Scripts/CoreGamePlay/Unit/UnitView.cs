using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Animator _animator;

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public void PlayDamage()
    {
        _animator.SetTrigger("Damage");
    }

    public void PlayDeath()
    {
        _animator.SetTrigger("Death");
    }
}