using System;
using UnityEngine;

namespace HybelStatSystem.Internal
{
    [Serializable]
    internal struct Optional<T>
    {
        public static readonly Optional<T> Disabled = new();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Assigned by Unity")]
        [SerializeField] private bool enabled;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Assigned by Unity")]
        [SerializeField] private T value;

        public Optional(T initialValue, bool enabled = true)
        {
            this.enabled = enabled;
            value = initialValue;
        }

        public bool Enabled => enabled;
        public T Value => value;

        public static implicit operator T(Optional<T> optional) => optional.Enabled ? optional.Value : default;
        public static implicit operator Optional<T>(T value) => new Optional<T>(value);
        public static implicit operator Optional<T>((T, bool) tuple) => new Optional<T>(tuple.Item1, tuple.Item2);
    }
}
