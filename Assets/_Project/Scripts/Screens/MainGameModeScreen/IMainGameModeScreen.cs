
using System;
using UniRx;

public interface IMainGameModeScreen : IUiScreen
{
    public IObservable<Unit> OnMenu { get; }
    public IObservable<Unit> OnBattleButton { get; }
    public IObservable<Unit> OnPauseButton { get; }
}

