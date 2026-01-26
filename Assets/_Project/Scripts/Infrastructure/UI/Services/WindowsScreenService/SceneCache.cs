using System;
using System.Collections.Generic;
using CodeBase.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneCache<TSceneController>
        where TSceneController : class
{
    private readonly Dictionary<Type, AssetReference> _scenes;
    private readonly Dictionary<Type, (TSceneController, AsyncOperationHandle<SceneInstance>)> _loadedScenes;
    private readonly HashSet<Type> _nonUnloadableScenes;

    private Func<TSceneController, UniTask> _onLoad;
    private Func<TSceneController, UniTask> _onUnload;

    public SceneCache()
    {
        _scenes = new();
        _loadedScenes = new();
        _nonUnloadableScenes = new();
    }
    public void SetOnLoadAction(Func<TSceneController, UniTask> onLoad)
    {
        _onLoad = onLoad;
    }
    public void SetOnUnloadAction(Func<TSceneController, UniTask> onUnload)
    {
        _onUnload = onUnload;
    }
    public void RegisterScene<T>(AssetReference sceneReference)
    {
        Type sceneType = typeof(T);
        if (_scenes.ContainsKey(sceneType))
        {
            throw new Exception($"Scene {sceneType} is already registered");
        }
        _scenes[typeof(T)] = sceneReference;
    }

    public async UniTask<T> GetSceneController<T>()
        where T : class, TSceneController
    {
        Type sceneType = typeof(T);
        if (_loadedScenes.TryGetValue(sceneType, out var scene))
        {
            return scene.Item1 as T;
        }

        return await LoadScene<T>();
    }
    public async UniTask ReleaseScene<T>()
        where T : class, TSceneController
    {
        Type sceneType = typeof(T);
        if (!_nonUnloadableScenes.Contains(sceneType))
        {
            await UnloadScene<T>();
        }
    }
    public void MakeNonUnloadable<T>()
    where T : class, TSceneController
    {
        Type sceneType = typeof(T);
        _nonUnloadableScenes.Add(sceneType);
    }
    public async UniTask LoadAndMakeNonUnloadable<T>()
        where T : class, TSceneController
    {
        Type sceneType = typeof(T);
        if (!_nonUnloadableScenes.Contains(sceneType))
        {
            _nonUnloadableScenes.Add(sceneType);
        }

        await LoadScene<T>();
    }
    private async UniTask<T> LoadScene<T>()
        where T : class, TSceneController
    {
        Type screenType = typeof(T);

        AsyncOperationHandle<SceneInstance> loadingOperation = Addressables.LoadSceneAsync(_scenes[screenType], LoadSceneMode.Additive);
        
        T scene = (await loadingOperation).Scene.GetRoot<T>();
        _loadedScenes[screenType] = (scene, loadingOperation);
        if (_onLoad != null)
        {
            await _onLoad(scene);
        }
        return scene;
    }
    private async UniTask UnloadScene<T>()
    {
        Type screenType = typeof(T);

        if (_loadedScenes.TryGetValue(screenType, out var loadedSceneEntry))
        {
            if (_onUnload != null)
            {
                await _onUnload(loadedSceneEntry.Item1);
            }
            await Addressables.UnloadSceneAsync(loadedSceneEntry.Item2);
        }
        _loadedScenes.Remove(screenType);
    }
}