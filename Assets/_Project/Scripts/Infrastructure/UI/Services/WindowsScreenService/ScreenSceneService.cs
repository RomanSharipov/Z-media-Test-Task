using Cysharp.Threading.Tasks;
using System;
using VContainer;

public class ScreenSceneService : IScreenSceneService
{
    private ResourceManager _singleResourceManager;

    [Inject]
    public ScreenSceneService(ResourceProvider resourceProvider)
    {
        _singleResourceManager = new ResourceManager(resourceProvider);
    }

    public UniTask HidePopup<TScreen>() where TScreen : class, IUiScreen
    {
        return _singleResourceManager.HideScreen<TScreen>();
    }

    public UniTask<TScreen> ShowPopup<TScreen>(Action<TScreen> setup = null) where TScreen : class, IUiScreen
    {
        return _singleResourceManager.ShowScreen<TScreen>(setup);
    }
}

