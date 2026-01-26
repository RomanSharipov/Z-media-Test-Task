using Cysharp.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;


public class AddressableSceneResourceProvider<T> : ITypedResourceProvider<T> where T : class
{
    private SceneCache<T> _sceneCache;

    public AddressableSceneResourceProvider()
    {
        _sceneCache = new();
    }
    public void RegisterScene<TResource>(AssetReference assetReference) where TResource : class, T
    {
        _sceneCache.RegisterScene<TResource>(assetReference);
    }
    public UniTask<TResource> GetResource<TResource>() where TResource : class, T
    {
        return _sceneCache.GetSceneController<TResource>();
    }
    public UniTask ReturnResource<TResource>() where TResource : class, T
    {
        return _sceneCache.ReleaseScene<TResource>();
    }

    public void SetOnLoadAction(Func<T, UniTask> onLoad)
    {
        _sceneCache.SetOnLoadAction(onLoad);
    }

    public void SetOnUnloadAction(Func<T, UniTask> onUnload)
    {
        _sceneCache.SetOnUnloadAction(onUnload);
    }
}