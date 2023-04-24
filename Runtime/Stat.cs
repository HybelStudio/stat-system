using System;
using System.Collections.Generic;

namespace Hybel.StatSystem
{
    public class Stat : IStat
    {
        protected readonly List<StatModifier> _modifiers = new();

        protected float _baseValue = 0f;
        protected float _alterableBaseValue;

        protected bool _isDirty = true;
        protected float _value;

        public virtual float Value
        {
            get
            {
                if (ShouldRecalculateValue())
                {
                    _value = CalculateValue();
                    _isDirty = false;
                }

                return _value;
            }
        }

        public virtual float AlterableBaseValue
        {
            get => _alterableBaseValue;
            set
            {
                if (value == _alterableBaseValue)
                    return;

                _alterableBaseValue = value;
                _isDirty = true;
            }
        }

        public Stat(float initialValue)
        {
            _baseValue = initialValue;
            _alterableBaseValue = _baseValue;
        }

        public Stat(IStatType statType)
        {
            _baseValue = statType.DefaultValue;
            _alterableBaseValue = _baseValue;
        }

        protected virtual float CalculateValue()
        {
            float finalValue = _alterableBaseValue;
            float sumPercentAdditive = 0f;

            for (int i = 0; i < _modifiers.Count; i++)
            {
                StatModifier modifier = _modifiers[i];

                switch (modifier.ModifierType)
                {
                    case StatModifierType.Additive:
                        finalValue += modifier.Value;
                        break;

                    case StatModifierType.PercentAdditive:
                        sumPercentAdditive += modifier.Value;
                        if (IsLastPercentAdditiveModifier(i))
                            finalValue *= 1 + sumPercentAdditive;

                        break;

                    case StatModifierType.PercentMultiplicative:
                        finalValue *= 1 + modifier.Value;
                        break;
                }
            }

            return (float)Math.Round(finalValue, 4);

            bool IsLastPercentAdditiveModifier(int modifierIndex) =>
                modifierIndex + 1 >= _modifiers.Count ||
                _modifiers[modifierIndex + 1].ModifierType != StatModifierType.PercentAdditive;
        }

        public virtual void AddModifier(StatModifier modifier)
        {
            _isDirty = true;

            int index = _modifiers.BinarySearch(modifier, new ByPriority());

            if (index < 0)
                index = ~index;

            _modifiers.Insert(index, modifier);
        }

        public virtual bool RemoveModifier(StatModifier modifier)
        {
            if (_modifiers.Remove(modifier))
            {
                _isDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = _modifiers.Count - 1; i >= 0; i--)
            {
                if (_modifiers[i].Source == source)
                {
                    _isDirty = true;
                    didRemove = true;
                    _modifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        private bool ShouldRecalculateValue() =>
            _isDirty;

        public static implicit operator float(Stat stat) => stat.Value;

        protected class ByPriority : IComparer<StatModifier>
        {
            public int Compare(StatModifier x, StatModifier y)
            {
                if (x.Order > y.Order)
                    return 1;

                if (x.Order < y.Order)
                    return -1;

                return 0;
            }
        }
    }
}