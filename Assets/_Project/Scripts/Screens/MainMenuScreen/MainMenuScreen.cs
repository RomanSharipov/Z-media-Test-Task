using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : ABaseScreen, IMainMenuScreen
{
    [SerializeField]
    private Button _playButton;
    
    public IObservable<Unit> OnPlay => _playButton.OnClickAsObservable();
    
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
