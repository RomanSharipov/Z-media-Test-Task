using UnityEngine;

[CreateAssetMenu(menuName = "BattleSim/BattleConfig", fileName = "BattleConfig")]
public class BattleConfig : ScriptableObject
{
    [field: SerializeField] public int UnitsPerTeam { get; private set; } = 20;
    [field: SerializeField] public float SpawnSpread { get; private set; } = 5f;

}