namespace Hybel.StatSystem
{
    public readonly struct StatModifier
    {
        private readonly float _value;
        private readonly int _order;
        private readonly object _source;

        private readonly StatModifierType _modifierType;

        public StatModifierType ModifierType => _modifierType;

        public float Value => _value;
        public int Order => _order;
        public object Source => _source;

        public StatModifier(StatModifierType modifierType, float value, int order, object source)
        {
            _value = value;
            _order = order;
            _source = source;

            _modifierType = modifierType;
        }

        public StatModifier(StatModifierType modifierType, float value) :
            this(modifierType, value, (int)modifierType, null)
        { }

        public StatModifier(StatModifierType modifierType, float value, int order) :
            this(modifierType, value, order, null)
        { }

        public StatModifier(StatModifierType modifierType, float value, object source) :
            this(modifierType, value, (int)modifierType, source)
        { }
    }
}