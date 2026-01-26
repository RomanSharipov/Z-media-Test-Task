using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class ABaseScreen : MonoBehaviour, IUiScreen
{
    [Header("Scene Objects")]
    [SerializeField]
    private AnimatedUIPanel _animatedUiPanel;

    private ReactiveProperty<bool> _onScreen;


    protected bool _initialized;

    public IReadOnlyReactiveProperty<bool> OnScreen => _onScreen;


    public virtual UniTask InitializeAsync()
    {
        if (_initialized)
        {
            throw new Exception("Already initialized");
        }

        _animatedUiPanel.Initialize();
        _initialized = true;
        _onScreen = new();
        return UniTask.CompletedTask;
    }

    public virtual async UniTask Show()
    {
        if (!_initialized)
        {
            throw new Exception("Not initialized");
        }
        await _animatedUiPanel.Show();
        _onScreen.Value = true;
    }
    public virtual async UniTask Hide()
    {
        if (!_initialized)
        {
            throw new Exception("Not initialized");
        }
        _onScreen.Value = false;
        await _animatedUiPanel.Hide();
    }
}