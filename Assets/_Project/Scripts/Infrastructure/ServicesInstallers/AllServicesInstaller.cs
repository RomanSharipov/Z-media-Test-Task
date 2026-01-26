using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "AllServicesInstaller",
    menuName = "ScriptableInstallers/AllServicesInstaller")]
    
    public class AllServicesInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<LevelService>(Lifetime.Singleton)
                .As<ILevelService>();
            
            builder.Register<InputService>(Lifetime.Singleton)
                .As<IInputService>()
                .As<IInitializable>();
            
            builder.Register<PlayerPrefsSaveService>(Lifetime.Singleton)
                .As<ISaveService>();
            
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
            
            builder.Register<AddressableAssetProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
        }
    }    
}
