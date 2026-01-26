using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UiScenes;
using UnityEngine;

[CreateAssetMenu(fileName = "Fade UI Animation", menuName = "UiScenes/DoTween Animations/Fade")]
public class FadeUIAnimation : ScriptableUIAnimation
{
    [SerializeField]
    private float _hideDuration = 0.3f;
    [SerializeField]
    private float _showDuration = 0.3f;
    [SerializeField]
    private Ease _ease;

    public override void Hide(RectTransform target)
    {
        CanvasGroup canvasGroup = target.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0.0f;
    }
    public override void Show(RectTransform target)
    {
        CanvasGroup canvasGroup = target.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1.0f;
    }

    public override UniTask HideAnimation(RectTransform target, CancellationToken cancellationToken)
    {
        CanvasGroup canvasGroup = target.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1.0f;

        

        return canvasGroup
            .DOFade(0.0f, _hideDuration)
            .SetEase(_ease)
            .SetUpdate(true)
            .ToUniTask(TweenCancelBehaviour.Complete, cancellationToken);
    }

    public override UniTask ShowAnimation(RectTransform target, CancellationToken cancellationToken)
    {
        CanvasGroup canvasGroup = target.gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0.0f;
        return canvasGroup
            .DOFade(1.0f, _showDuration)
            .SetEase(_ease)
            .SetUpdate(true)
            .ToUniTask(TweenCancelBehaviour.Complete, cancellationToken);
    }
}