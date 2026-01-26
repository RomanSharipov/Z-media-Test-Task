using CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "StatemachineInstaller",
    menuName = "ScriptableInstallers/StatemachineInstaller")]
    public class StatemachineInstaller : AScriptableInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            GameStateMachine mainGameStateMachine = new GameStateMachine();

            builder.Register<AppStateService>(Lifetime.Singleton)
                .WithParameter("mainGameStateMachine", mainGameStateMachine)
                .As<IAppStateService>();

            RegisterStates(builder);

            builder.RegisterEntryPoint<EntryPoint>();
        }

        private void RegisterStates(IContainerBuilder builder)
        {
            builder.Register<GameLoopState>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();
            builder.Register<BattleState>(Lifetime.Singleton)
                .AsSelf()
                .AsImplementedInterfaces();


            builder.Register<BootstrapState>(Lifetime.Singleton).AsSelf();
            builder.Register<MenuState>(Lifetime.Singleton).AsSelf();
            builder.Register<PauseState>(Lifetime.Singleton).AsSelf();
        }
    }
}