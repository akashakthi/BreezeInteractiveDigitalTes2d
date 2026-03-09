using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.Common.Combat
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
        Transform GetTransform();
    }
}