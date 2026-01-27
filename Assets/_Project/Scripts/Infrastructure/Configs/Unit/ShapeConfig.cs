using AYellowpaper.SerializedCollections;
using CodeBase.CoreGamePlay;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleSim/Modifiers/Shape", fileName = "NewShape")]
public class ShapeConfig : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public UnitView UnitViewPrefab { get; private set; }
    [field: SerializeField] public SerializedDictionary<StatType, float> Modifiers { get; private set; }
}