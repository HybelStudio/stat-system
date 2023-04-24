namespace Hybel.StatSystem
{
    public interface IStat
    {
        float AlterableBaseValue { get; set; }
        float Value { get; }

        void AddModifier(StatModifier modifier);
        bool RemoveAllModifiersFromSource(object source);
        bool RemoveModifier(StatModifier modifier);
    }
}