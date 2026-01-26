using CodeBase.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

[CreateAssetMenu(fileName = "UiServicesInstaller", menuName = "ScriptableInstallers/UiServicesInstaller")]
public class UiServicesInstaller : AScriptableInstaller
{
    [SerializeField, AssetReferenceUILabelRestriction("UiScreen")]
    private AssetReference _mainMenuScreen;
    [SerializeField, AssetReferenceUILabelRestriction("UiScreen")]
    private AssetReference _mainGameModeScreen;
    
    private ResourceProvider CreateNewScreensProvider(IObjectResolver resolver)
    {
        ResourceProvider provider = new ResourceProvider();


        provider.RegisterScene<IMainMenuScreen>(_mainMenuScreen);
        provider.RegisterScene<IMainGameModeScreen>(_mainGameModeScreen);


        return provider;
    }

    public override void Install(IContainerBuilder builder)
    {
        builder
            .Register<ScreenSceneService>(Lifetime.Singleton)
            .As<IScreenSceneService>();

        builder
            .Register<ResourceProvider>(CreateNewScreensProvider, Lifetime.Singleton);

    }

}
