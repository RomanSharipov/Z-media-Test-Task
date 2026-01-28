using CodeBase.Infrastructure.Installers;
using UnityEngine;
using VContainer;


[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "ScriptableInstallers/ConfigsInstaller")]

public class ConfigsInstaller : AScriptableInstaller
{
    [SerializeField] private WarriorConfigDatabase _unitConfigDatabase;
    [SerializeField] private BattleConfig _battleConfig;
    
    public override void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_unitConfigDatabase).AsSelf();
        builder.RegisterInstance(_battleConfig).AsSelf();

    }
}