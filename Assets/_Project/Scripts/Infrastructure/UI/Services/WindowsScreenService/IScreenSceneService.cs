using Cysharp.Threading.Tasks;
using System;

public interface IScreenSceneService
{
    public UniTask HidePopup<TScreen>() where TScreen : class, IUiScreen;
    public UniTask<TScreen> ShowPopup<TScreen>(Action<TScreen> setup = null) where TScreen : class, IUiScreen;
}

