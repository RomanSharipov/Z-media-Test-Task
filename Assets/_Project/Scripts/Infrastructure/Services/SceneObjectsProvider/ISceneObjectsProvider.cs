using CodeBase.CoreGamePlay;
using UnityEngine;

public interface ISceneObjectsProvider
{
    public Transform UnitsContainer { get; set; }
    public WarriorsSpawner WarriorsSpawner { get; set; }
}