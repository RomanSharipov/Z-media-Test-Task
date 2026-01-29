using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : ABaseScreen, IWinScreen
{
    [SerializeField]
    private Button _menuButton;
    
    public IObservable<Unit> OnMenu => _menuButton.OnClickAsObservable();

    

}
