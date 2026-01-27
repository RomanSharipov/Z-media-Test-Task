using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UiScenes
{
    [CreateAssetMenu(fileName = "Fade UI Animation", menuName = "UiScenes/DoTween Animations/Fade")]
    public class FadeUIAnimation : ScriptableUIAnimation
    {
        [SerializeField] private float _hideDuration = 0.3f;
        [SerializeField] private float _showDuration = 0.3f;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override void Hide(RectTransform target)
        {
            GetOrAddCanvasGroup(target).alpha = 0f;
        }

        public override void Show(RectTransform target)
        {
            GetOrAddCanvasGroup(target).alpha = 1f;
        }

        public override async UniTask HideAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            var canvasGroup = GetOrAddCanvasGroup(target);
            await AnimateAlpha(canvasGroup, 1f, 0f, _hideDuration, cancellationToken);
        }

        public override async UniTask ShowAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            var canvasGroup = GetOrAddCanvasGroup(target);
            await AnimateAlpha(canvasGroup, 0f, 1f, _showDuration, cancellationToken);
        }

        private async UniTask AnimateAlpha(CanvasGroup canvasGroup, float from, float to, float duration, CancellationToken cancellationToken)
        {
            canvasGroup.alpha = from;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                elapsed += Time.unscaledDeltaTime;
                float t = _curve.Evaluate(elapsed / duration);
                canvasGroup.alpha = Mathf.Lerp(from, to, t);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            canvasGroup.alpha = to;
        }

        private CanvasGroup GetOrAddCanvasGroup(RectTransform target)
        {
            var canvasGroup = target.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = target.gameObject.AddComponent<CanvasGroup>();
            return canvasGroup;
        }
    }
}