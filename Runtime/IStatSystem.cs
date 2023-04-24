namespace Hybel.StatSystem
{
    public interface IStatSystem
    {
        public void AddModifier(IStatType statType, StatModifier modifier);
        public void RemoveModifier(IStatType statType, StatModifier modifier);
        public IStat GetStat(IStatType statType);
        public IStat GetStat(string statTypeName);
        public IObservableStat GetObservableStat(IStatType statType);
        public IObservableStat GetObservableStat(string statTypeName);
    }
}