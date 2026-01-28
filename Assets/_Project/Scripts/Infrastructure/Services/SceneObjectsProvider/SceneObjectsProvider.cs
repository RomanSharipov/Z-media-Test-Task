using CodeBase.CoreGamePlay;
using UnityEngine;

public class SceneObjectsProvider : ISceneObjectsProvider
{
    public Transform UnitsContainer {  get; set; }
    public WarriorsSpawner WarriorsSpawner { get; set; }
}