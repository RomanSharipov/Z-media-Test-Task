using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VContainer;

public class MainGameModeScreen : ABaseScreen, IMainGameModeScreen
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _battleButton;
    [SerializeField] private Button _randomizerButton;
    [SerializeField] private TMP_Text _playerUnitsCount;
    [SerializeField] private TMP_Text _botUnitsCount;

    [Inject] private IWarriorsOnLevel _warriorsOnLevel;

    public IObservable<Unit> OnMenu => _menuButton.OnClickAsObservable();
    public IObservable<Unit> OnBattleButton => _battleButton.OnClickAsObservable();
    public IObservable<Unit> OnRandomizerButton => _randomizerButton.OnClickAsObservable();

    public override UniTask InitializeAsync()
    {
        _battleButton.OnClickAsObservable().Subscribe(_ =>
        {
            _battleButton.gameObject.SetActive(false);
            _randomizerButton.gameObject.SetActive(false);
        }).AddTo(this);

        _warriorsOnLevel.OnWarriorCountChanged
            .Subscribe(_ => UpdateCounters())
            .AddTo(this);

        UpdateCounters();

        return base.InitializeAsync();
    }

    public void UpdateCounters()
    {
        _playerUnitsCount.SetText($"Player: {_warriorsOnLevel.PlayerWarriors.Count}");
        _botUnitsCount.SetText($"Bot: {_warriorsOnLevel.BotWarriors.Count}");
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