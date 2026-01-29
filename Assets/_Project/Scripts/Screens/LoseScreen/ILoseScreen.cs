
using System;
using UniRx;

public interface ILoseScreen : IUiScreen
{
    public IObservable<Unit> OnMenu { get; }

}

