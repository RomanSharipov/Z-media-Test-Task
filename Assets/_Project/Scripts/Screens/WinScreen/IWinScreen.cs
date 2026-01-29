
using System;
using UniRx;

public interface IWinScreen : IUiScreen
{
    public IObservable<Unit> OnMenu { get; }

}

