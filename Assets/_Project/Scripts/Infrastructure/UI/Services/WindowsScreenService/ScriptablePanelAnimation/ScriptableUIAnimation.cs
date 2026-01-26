using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace UiScenes
{
    public abstract class ScriptableUIAnimation : ScriptableObject
    {
        public abstract void Show(RectTransform target);
        public abstract void Hide(RectTransform target);
        public abstract UniTask ShowAnimation(RectTransform target, CancellationToken cancellationToken);
        public abstract UniTask HideAnimation(RectTransform target, CancellationToken cancellationToken);
    }
}