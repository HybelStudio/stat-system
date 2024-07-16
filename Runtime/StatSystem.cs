using System;
using System.Collections.Generic;

namespace Hybel.StatSystem
{
    public class StatSystem : IStatSystem
    {
        private readonly Dictionary<IStatType, IStat> _statsDictionary = new();

        public IReadOnlyDictionary<IStatType, IStat> Stats => _statsDictionary;

        public StatSystem(BaseStats baseStats)
        {
            foreach (var stat in baseStats.Stats)
                _statsDictionary.Add(stat.StatType, new Stat(stat.BaseValue));
        }

        public void AddModifier(IStatType statType, StatModifier modifier)
        {
            if (!_statsDictionary.TryGetValue(statType, out IStat stat))
            {
                stat = new Stat(statType);
                _statsDictionary.Add(statType, stat);
            }

            stat.AddModifier(modifier);
        }

        public bool RemoveModifier(IStatType statType, StatModifier modifier)
        {
            if (!_statsDictionary.TryGetValue(statType, out IStat stat))
                return false;

            return stat.RemoveModifier(modifier);
        }

        public IStat GetStat(IStatType statType)
        {
            if (!_statsDictionary.TryGetValue(statType, out IStat stat))
            {
                stat = new Stat(statType);
                _statsDictionary.Add(statType, stat);
            }

            return stat;
        }

        public IStat GetStat(string statTypeName)
        {
            foreach (var statEntry in _statsDictionary)
                if (statEntry.Key.Name.ToLower().Contains(statTypeName.ToLower()))
                    return statEntry.Value;

            throw new InvalidOperationException($"Stat with name {statTypeName} does not exist in {this}'s stats dictionary.");
        }

        public IObservableStat GetObservableStat(IStatType statType)
        {
            if (!_statsDictionary.TryGetValue(statType, out IStat stat))
            {
                stat = new Stat(statType);
                _statsDictionary.Add(statType, stat);
            }

            return new ObservableStat(stat);
        }

        public IObservableStat GetObservableStat(string statTypeName)
        {
            foreach (var statEntry in _statsDictionary)
                if (statEntry.Key.Name.ToLower().Contains(statTypeName.ToLower()))
                    return new ObservableStat(statEntry.Value);

            throw new InvalidOperationException($"Stat with name {statTypeName} does not exist in {this}'s stats dictionary.");
        }
    }
}