
using System;
using UniRx;

public interface IMainMenuScreen : IUiScreen
{
    public IObservable<Unit> OnPlay { get; }
    public IObservable<Unit> OnSettings { get; }
}

