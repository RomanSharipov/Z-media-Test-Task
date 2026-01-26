using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

public static class UnitDataBuilder
{
    public static UnitData Build(
        BaseStatsConfig baseStats,
        ShapeConfig shape,
        SizeConfig size,
        ColorConfig color)
    {
        var combined = new SerializedDictionary<StatType, float>();

        ApplyModifiers(combined, baseStats.Stats);
        ApplyModifiers(combined, shape.Modifiers);
        ApplyModifiers(combined, size.Modifiers);
        ApplyModifiers(combined, color.Modifiers);

        return new UnitData
        {
            HP = combined.GetValueOrDefault(StatType.HP),
            ATK = combined.GetValueOrDefault(StatType.ATACK),
            Speed = combined.GetValueOrDefault(StatType.SPEED),
            AttackSpeed = combined.GetValueOrDefault(StatType.ATACK_SPEED)
        };
    }

    private static void ApplyModifiers(
        SerializedDictionary<StatType, float> target,
        SerializedDictionary<StatType, float> modifiers)
    {
        foreach (var kvp in modifiers)
        {
            if (target.ContainsKey(kvp.Key))
                target[kvp.Key] += kvp.Value;
            else
                target[kvp.Key] = kvp.Value;
        }
    }
}