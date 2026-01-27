using CodeBase.Infrastructure.Installers;
using UnityEngine;
using VContainer;


[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "ScriptableInstallers/ConfigsInstaller")]

public class ConfigsInstaller : AScriptableInstaller
{
    [SerializeField] private UnitConfigDatabase _unitConfigDatabase;
    
    public override void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_unitConfigDatabase).AsSelf();

    }
}