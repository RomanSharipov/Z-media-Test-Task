using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleSim/Modifiers/Size", fileName = "NewSize")]
public class SizeConfig : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public float Scale { get; private set; } = 1f;
    [field: SerializeField] public SerializedDictionary<StatType, float> Modifiers { get; private set; }
}