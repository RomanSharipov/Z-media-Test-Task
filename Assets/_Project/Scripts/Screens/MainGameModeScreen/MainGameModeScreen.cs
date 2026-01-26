using Codice.Client.BaseCommands.Merge;
using Cysharp.Threading.Tasks;
using System;
using TriInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class MainGameModeScreen : ABaseScreen, IMainGameModeScreen
{
    [SerializeField]
    private Button _menuButton;
    [SerializeField]
    private Button _battleButton;
    [SerializeField]
    private Button _pauseButton;
    [Inject]
    private ISaveService ISaveService;
    
    public IObservable<Unit> OnMenu => _menuButton.OnClickAsObservable();
    public IObservable<Unit> OnBattleButton => _battleButton.OnClickAsObservable();
    public IObservable<Unit> OnPauseButton => _pauseButton.OnClickAsObservable();

    [Button]
    public void Prit()
    {
        Debug.Log($"ISaveService = {ISaveService}");
    }

    public override UniTask InitializeAsync()
    {
        return base.InitializeAsync();
    }
    public override UniTask Hide()
    {
        return base.Hide();
    }
    public override UniTask Show()
    {
        return base.Show();
    }
}
