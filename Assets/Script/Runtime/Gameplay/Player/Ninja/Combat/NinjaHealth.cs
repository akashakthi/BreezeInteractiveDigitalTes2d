using System;
using System.Collections;
using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Common.Combat;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat
{
    public sealed class NinjaHealth : MonoBehaviour, IDamageable, IHealthSource
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private GameObject healthBarRoot;
        [SerializeField] private bool destroyOwnerOnDeath = false;
        [SerializeField] private float destroyDelay = 0.1f;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0;

        public event Action<int, int> HealthChanged;
        public event Action<int> Damaged;
        public event Action Died;

        private bool _isDeathProcessing;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
            _isDeathProcessing = false;
            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamage(int amount)
        {
            if (IsDead || amount <= 0 || _isDeathProcessing)
            {
                return;
            }

            CurrentHealth -= amount;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            HealthChanged?.Invoke(CurrentHealth, MaxHealth);
            Damaged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                StartCoroutine(HandleDeathRoutine());
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }

        private IEnumerator HandleDeathRoutine()
        {
            if (_isDeathProcessing)
            {
                yield break;
            }

            _isDeathProcessing = true;

            Died?.Invoke();

            if (healthBarRoot != null)
            {
                Destroy(healthBarRoot);
            }

            if (destroyOwnerOnDeath)
            {
                yield return new WaitForSeconds(destroyDelay);
                Destroy(gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (maxHealth < 1)
            {
                maxHealth = 1;
            }

            if (destroyDelay < 0f)
            {
                destroyDelay = 0f;
            }
        }
#endif
    }
}