using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleSim/Modifiers/Color", fileName = "NewColor")]
public class ColorConfig : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField] public SerializedDictionary<StatType, float> Modifiers { get; private set; }
}