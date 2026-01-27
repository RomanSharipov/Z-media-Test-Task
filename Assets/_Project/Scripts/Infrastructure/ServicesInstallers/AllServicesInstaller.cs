using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "AllServicesInstaller",menuName = "ScriptableInstallers/AllServicesInstaller")]
    
    public class AllServicesInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<LevelService>(Lifetime.Singleton)
                .As<ILevelService>();
            
            builder.Register<PlayerPrefsSaveService>(Lifetime.Singleton)
                .As<ISaveService>();
            
            builder.Register<SceneLoader>(Lifetime.Singleton)
                .As<ISceneLoader>();
            
            builder.Register<AddressableAssetProvider>(Lifetime.Singleton)
                .As<IAssetProvider>();
            
            builder.Register<UnitFactory>(Lifetime.Singleton)
                .As<IUnitFactory>();
            
            builder.Register<SceneObjectsProvider>(Lifetime.Singleton)
                .As<ISceneObjectsProvider>();
            
            builder.Register<UnitsOnLevel>(Lifetime.Singleton)
                .As<IUnitsOnLevel>();
        }
    }    
}
