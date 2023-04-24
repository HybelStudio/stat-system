using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hybel.StatSystem
{
    public static class StatModifierExtensions
    {
        public static string ToStatText(this StatModifier statModifier)
        {
            float value = statModifier.Value;
            StatModifierType modifierType = statModifier.ModifierType;

            return modifierType switch
            {
                StatModifierType.Additive => value.ToString(),
                StatModifierType.PercentAdditive => $"{(Mathf.Sign(value) == 1 ? "+" : "-")}{value * 100f}%",
                StatModifierType.PercentMultiplicative => value.ToString(),
                _ => string.Empty,
            };
        }
    }
}