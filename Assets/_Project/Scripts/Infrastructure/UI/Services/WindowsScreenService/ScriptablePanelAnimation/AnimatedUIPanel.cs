using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class AnimatedUIPanel : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    private Canvas _targetCanvas;
    [SerializeField]
    private RectTransform _panel;
    [Header("Parameters")]
    [SerializeField]
    private AnimatedUIPanelParameters _parameters;


    private bool _initialized;

    private Subject<Unit> _onShowStart;
    private Subject<Unit> _onShowEnd;
    private Subject<Unit> _onHideStart;
    private Subject<Unit> _onHideEnd;

    private CancellationTokenSource _animationCancellationTokenSource;

    public IObservable<Unit> OnShowStart => _onShowStart;
    public IObservable<Unit> OnShowEnd => _onShowEnd;
    public IObservable<Unit> OnHideStart => _onHideStart;
    public IObservable<Unit> OnHideEnd => _onHideEnd;

    public void Initialize()
    {
        if (_initialized) throw new Exception("Already initialized");

        _panel.gameObject.SetActive(false);
        _onShowStart = new();
        _onShowEnd = new();
        _onHideStart = new();
        _onHideEnd = new();

        _initialized = true;
    }
    private void OnDestroy()
    {
        DOTween.Kill(_panel.gameObject.GetComponent<CanvasGroup>(), complete: true);
        _animationCancellationTokenSource?.Cancel();
    }
    public async virtual UniTask Show(bool skipAnimation = false)
    {
        if (!_initialized) throw new Exception("UI panel not initialized");

        _animationCancellationTokenSource?.Cancel();
        _animationCancellationTokenSource = new();

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

        await _parameters.Animation.ShowAnimation(_panel, _animationCancellationTokenSource.Token);

        _onShowEnd.OnNext(Unit.Default);
    }

    public async virtual UniTask Hide(bool skipAnimation = false)
    {
        if (!_initialized) throw new Exception("UI panel not initialized");

        _animationCancellationTokenSource?.Cancel();
        _animationCancellationTokenSource = new();

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
        await _parameters.Animation.HideAnimation(_panel, _animationCancellationTokenSource.Token);
        _panel.gameObject.SetActive(false);
        _onHideEnd.OnNext(Unit.Default);
    }
}