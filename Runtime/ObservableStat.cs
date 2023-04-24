using System;

namespace Hybel.StatSystem
{
    public class ObservableStat : IObservableStat
    {
        public event Action<float> ValueChanged;

        private readonly IStat _stat;

        public ObservableStat(IStat stat) => _stat = stat;

        public float Value => _stat.Value;

        public float AlterableBaseValue
        {
            get => _stat.AlterableBaseValue;
            set
            {
                if (value == _stat.AlterableBaseValue)
                    return;

                _stat.AlterableBaseValue = value;
                ValueChanged?.Invoke(Value);
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _stat.AddModifier(modifier);
            ValueChanged?.Invoke(Value);
        }

        public bool RemoveModifier(StatModifier modifier)
        {
            bool didRemove = _stat.RemoveModifier(modifier);

            if (didRemove)
                ValueChanged?.Invoke(Value);

            return didRemove;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = _stat.RemoveAllModifiersFromSource(source);

            if (didRemove)
                ValueChanged?.Invoke(Value);

            return didRemove;
        }

        public static implicit operator float(ObservableStat stat) => stat.Value;
    }
}