using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MainGameModeScreen : ABaseScreen, IMainGameModeScreen
{
    [SerializeField]
    private Button _menuButton;
    [SerializeField]
    private Button _battleButton;
    [SerializeField]
    private Button _randomizerButton;
    
    public IObservable<Unit> OnMenu => _menuButton.OnClickAsObservable();
    public IObservable<Unit> OnBattleButton => _battleButton.OnClickAsObservable();
    public IObservable<Unit> OnRandomizerButton => _randomizerButton.OnClickAsObservable();
    
    public override UniTask InitializeAsync()
    {
        _battleButton.OnClickAsObservable().Subscribe(_ => 
        {
            _battleButton.gameObject.SetActive(false);
        }).AddTo(this);

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
