using Cysharp.Threading.Tasks;
using System;
using VContainer;

public interface ITypedResourceProvider<T> where T : class
{
    /// <summary>
    /// Loads resource. If resource was already loaded - does nothing.
    /// </summary>
    public UniTask<TResource> GetResource<TResource>() where TResource : class, T;
    /// <summary>
    /// Unloads resource. If resource wasn't loaded - does nothing.
    /// </summary>
    public UniTask ReturnResource<TResource>() where TResource : class, T;
    public void SetOnLoadAction(Func<T, UniTask> onLoad);
    public void SetOnUnloadAction(Func<T, UniTask> onUnload);
}

