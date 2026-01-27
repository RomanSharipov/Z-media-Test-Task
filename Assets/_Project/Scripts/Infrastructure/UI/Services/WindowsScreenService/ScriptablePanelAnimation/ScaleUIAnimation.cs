using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UiScenes
{
    [CreateAssetMenu(fileName = "Scale UI Animation", menuName = "UiScenes/DoTween Animations/Scale")]
    public class ScaleUIAnimation : ScriptableUIAnimation
    {
        [SerializeField] private float _hideDuration = 0.3f;
        [SerializeField] private float _showDuration = 0.3f;
        [SerializeField] private float _showScale = 1f;
        [SerializeField] private float _hideScale = 0f;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override void Hide(RectTransform target)
        {
            target.localScale = Vector3.one * _hideScale;
        }

        public override void Show(RectTransform target)
        {
            target.localScale = Vector3.one * _showScale;
        }

        public override async UniTask HideAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            await AnimateScale(target, _showScale, _hideScale, _hideDuration, cancellationToken);
        }

        public override async UniTask ShowAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            await AnimateScale(target, _hideScale, _showScale, _showDuration, cancellationToken);
        }

        private async UniTask AnimateScale(RectTransform target, float from, float to, float duration, CancellationToken cancellationToken)
        {
            target.localScale = Vector3.one * from;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                cancellationToken.ThrowIfCancellationRequested();

                elapsed += Time.unscaledDeltaTime;
                float t = _curve.Evaluate(elapsed / duration);
                float scale = Mathf.Lerp(from, to, t);
                target.localScale = Vector3.one * scale;

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            target.localScale = Vector3.one * to;
        }
    }
}