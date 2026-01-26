using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;


public class ResourceManager
{
    private readonly ResourceProvider _resourceProvider;
    protected IUiScreen _currentResource;
    private Dictionary<Type, IUiScreen> _openingPopups = new Dictionary<Type, IUiScreen>();

    public ResourceManager(ResourceProvider resourceProvider)
    {
        _resourceProvider = resourceProvider;
        _resourceProvider.SetOnLoadAction(OnLoadAction);
    }

    private UniTask OnLoadAction(IUiScreen screen)
    {
        return screen.InitializeAsync();
    }

    public async UniTask<TResource> ShowScreen<TResource>(Action<TResource> resourceSetup = null) where TResource : class, IUiScreen
    {
        if (_openingPopups.TryGetValue(typeof(TResource), out IUiScreen uiScreen))
        {
            TResource currentScene = (TResource)uiScreen;
            resourceSetup?.Invoke(currentScene);
            return currentScene;
        }

        TResource nextScene = await _resourceProvider.GetResource<TResource>();

        resourceSetup?.Invoke(nextScene);

        await nextScene.Show();
        _currentResource = nextScene;
        _openingPopups[typeof(TResource)] = _currentResource as TResource;
        return _currentResource as TResource;
    }

    public async UniTask HideScreen<TResource>() where TResource : class, IUiScreen
    {
        if (_openingPopups.TryGetValue(typeof(TResource), out IUiScreen uiScreen))
        {
            await uiScreen.Hide();
            await _resourceProvider.ReturnResource<TResource>();
            _openingPopups.Remove(typeof(TResource));
        }
    }
}