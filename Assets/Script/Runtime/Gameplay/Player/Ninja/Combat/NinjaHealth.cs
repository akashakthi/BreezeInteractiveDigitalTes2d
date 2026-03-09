using System;
using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat
{
    public sealed class NinjaHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0;

        public event Action<int> Damaged;
        public event Action Died;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0)
            {
                return;
            }

            CurrentHealth -= amount;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            Damaged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                Died?.Invoke();
            }
        }
    }
}