using AYellowpaper.SerializedCollections;
using System.Collections.Generic;

namespace CodeBase.CoreGamePlay
{
    public static class UnitDataBuilder
    {
        private const float MinAttackInterval = 0.1f;
        private const float MinHP = 1f;

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
                HP = UnityEngine.Mathf.Max(combined.GetValueOrDefault(StatType.HP), MinHP),
                Attack = UnityEngine.Mathf.Max(combined.GetValueOrDefault(StatType.ATACK), 0f),
                Speed = UnityEngine.Mathf.Max(combined.GetValueOrDefault(StatType.SPEED), 0f),
                AttackSpeed = UnityEngine.Mathf.Max(combined.GetValueOrDefault(StatType.ATACK_SPEED), MinAttackInterval)
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
}