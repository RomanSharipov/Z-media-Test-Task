using CodeBase.CoreGamePlay;
using UnityEngine;

[CreateAssetMenu(menuName = "BattleSim/Database", fileName = "UnitConfigDatabase")]
public class UnitConfigDatabase : ScriptableObject
{
    [field: SerializeField] public BaseStatsConfig BaseStats { get; private set; }
    [field: SerializeField] public Warrior UnitPrefab { get; private set; }
    [field: SerializeField] public ShapeConfig[] Shapes { get; private set; }
    [field: SerializeField] public SizeConfig[] Sizes { get; private set; }
    [field: SerializeField] public ColorConfig[] Colors { get; private set; }
}