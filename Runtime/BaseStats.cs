using HybelStatSystem.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hybel.StatSystem
{
    [CreateAssetMenu(fileName = "New Base Stats", menuName = "Stats/Base Stats")]
    public class BaseStats : ScriptableObject
    {
        [SerializeField] private List<BaseStat> stats;

        public List<BaseStat> Stats => stats;
    }

    [Serializable]
    public class BaseStat
    {
#if ODIN_INSPECTOR
        [InlineEditor]
#endif
        [SerializeField, SerializeReference] private StatType statType;
        [SerializeField] private Optional<float> newBaseValue;

        public StatType StatType => statType;
        public float BaseValue => newBaseValue.Enabled ? newBaseValue.Value : statType.DefaultValue;
    }
}
