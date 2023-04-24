using UnityEngine;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Hybel.StatSystem
{
    public class StatSystemBehaviour : MonoBehaviour, IStatSystem, IHasStats
    {
#if ODIN_INSPECTOR
        [InlineEditor]
#endif
        [SerializeField] private BaseStats baseStats;

        private StatSystem _statSystem;

        public IStatSystem StatSystem => _statSystem;

        private void Awake() => _statSystem = new StatSystem(baseStats);

#if ODIN_INSPECTOR
        [Button("Log Stats")]
#endif
        [ContextMenu("Log Stats")]
        private void LogStats()
        {
            foreach (KeyValuePair<IStatType, IStat> statsEntry in _statSystem.Stats)
#if CLOGGER
                this.LogDebug(statsEntry.Key.name, statsEntry.Value.Value);
#elif UNITY_EDITOR
                Debug.Log($"{{ StatType: {statsEntry.Key.Name}, Value: {statsEntry.Value.Value} }}");
#endif
        }

        public void AddModifier(IStatType statType, StatModifier modifier) => StatSystem.AddModifier(statType, modifier);
        public void RemoveModifier(IStatType statType, StatModifier modifier) => StatSystem.RemoveModifier(statType, modifier);
        public IStat GetStat(IStatType statType) => StatSystem.GetStat(statType);
        public IStat GetStat(string statTypeName) => StatSystem.GetStat(statTypeName);
        public IObservableStat GetObservableStat(IStatType statType) => StatSystem.GetObservableStat(statType);
        public IObservableStat GetObservableStat(string statTypeName) => StatSystem.GetObservableStat(statTypeName);
    }
}