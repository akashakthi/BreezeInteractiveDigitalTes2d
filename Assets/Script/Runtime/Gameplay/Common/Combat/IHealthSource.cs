using System;

namespace BreezeInteractive.Runtime.Gameplay.Common.Combat
{
    public interface IHealthSource
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }

        event Action<int, int> HealthChanged;
        event Action Died;
    }
}