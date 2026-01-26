using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace UiScenes
{
    [CreateAssetMenu(fileName = "Fade UI Animation", menuName = "UiScenes/DoTween Animations/Scale")]
    public class ScaleUIAnimation : ScriptableUIAnimation
    {
        [SerializeField]
        private float _hideDuration = 0.3f;
        [SerializeField]
        private float _showDuration = 0.3f;
        [SerializeField]
        private float _showScale = 1f;
        [SerializeField]
        private float _hideScale = 0f;
        [SerializeField]
        private Ease _ease;

        public override void Hide(RectTransform target)
        {
            target.localScale = new Vector3(_hideScale, _hideScale, _hideScale);
        }
        public override void Show(RectTransform target)
        {
            target.localScale = new Vector3(_showScale, _showScale, _showScale);
        }

        public override UniTask HideAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            return target
                .DOScale(_hideScale, _hideDuration)
                .SetUpdate(true)
                .ToUniTask(TweenCancelBehaviour.Complete, cancellationToken);
        }

        public override UniTask ShowAnimation(RectTransform target, CancellationToken cancellationToken)
        {
            target.localScale = Vector3.zero;
            return target
                .DOScale(_showScale, _showDuration)
                .SetUpdate(true)
                .ToUniTask(TweenCancelBehaviour.Complete, cancellationToken);
        }
    }
}