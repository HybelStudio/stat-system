using System;

namespace Hybel.StatSystem
{
    public interface IObservableStat : IStat
    {
        public event Action<float> ValueChanged;
    }
}