using UnityEngine;

public class UnitView : MonoBehaviour
{
    private Renderer _renderer;
    private Animator _animator;

    public void Initialize(Renderer renderer, Animator animator)
    {
        _renderer = renderer;
        _animator = animator;
    }

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

}