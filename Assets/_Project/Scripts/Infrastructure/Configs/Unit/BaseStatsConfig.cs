using AYellowpaper.SerializedCollections;
using UnityEngine;


[CreateAssetMenu(menuName = "BattleSim/Base Stats", fileName = "BaseStats")]
public class BaseStatsConfig : ScriptableObject
{
    [field: SerializeField] public SerializedDictionary<StatType, float> Stats { get; private set; }
}
