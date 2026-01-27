using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class AnimatedUIPanel : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Canvas _targetCanvas;
    [SerializeField] private RectTransform _panel;

    [Header("Parameters")]
    [SerializeField] private AnimatedUIPanelParameters _parameters;

    private bool _initialized;
    private CancellationTokenSource _animationCts;

    private Subject<Unit> _onShowStart = new();
    private Subject<Unit> _onShowEnd = new();
    private Subject<Unit> _onHideStart = new();
    private Subject<Unit> _onHideEnd = new();

    public IObservable<Unit> OnShowStart => _onShowStart;
    public IObservable<Unit> OnShowEnd => _onShowEnd;
    public IObservable<Unit> OnHideStart => _onHideStart;
    public IObservable<Unit> OnHideEnd => _onHideEnd;

    public void Initialize()
    {
        if (_initialized) throw new Exception("Already initialized");

        _panel.gameObject.SetActive(false);
        _initialized = true;
    }

    private void OnDestroy()
    {
        _animationCts?.Cancel();
        _animationCts?.Dispose();
    }

    public virtual async UniTask Show(bool skipAnimation = false)
    {
        if (!_initialized) throw new Exception("UI panel not initialized");

        _animationCts?.Cancel();
        _animationCts = new CancellationTokenSource();

        _panel.gameObject.SetActive(true);
        _targetCanvas.sortingOrder = _parameters.ForegroundSortOrder;

        if (skipAnimation)
        {
            _parameters.Animation.Show(_panel);
            _onShowStart.OnNext(Unit.Default);
            _onShowEnd.OnNext(Unit.Default);
            return;
        }

        _onShowStart.OnNext(Unit.Default);
        await _parameters.Animation.ShowAnimation(_panel, _animationCts.Token);
        _onShowEnd.OnNext(Unit.Default);
    }

    public virtual async UniTask Hide(bool skipAnimation = false)
    {
        if (!_initialized) throw new Exception("UI panel not initialized");

        _animationCts?.Cancel();
        _animationCts = new CancellationTokenSource();

        _targetCanvas.sortingOrder = _parameters.BackgroundSortOrder;

        if (skipAnimation)
        {
            _parameters.Animation.Hide(_panel);
            _panel.gameObject.SetActive(false);
            _onHideStart.OnNext(Unit.Default);
            _onHideEnd.OnNext(Unit.Default);
            return;
        }

        _onHideStart.OnNext(Unit.Default);
        await _parameters.Animation.HideAnimation(_panel, _animationCts.Token);
        _panel.gameObject.SetActive(false);
        _onHideEnd.OnNext(Unit.Default);
    }
}