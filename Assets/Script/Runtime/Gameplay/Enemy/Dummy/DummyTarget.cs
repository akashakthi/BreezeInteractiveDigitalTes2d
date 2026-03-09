using System;
using System.Collections;
using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Common.Combat;

namespace BreezeInteractive.Runtime.Gameplay.Enemy.Dummy
{
    public sealed class DummyTarget : MonoBehaviour, IDamageable, IHealthSource
    {
        [SerializeField] private int maxHealth = 200;
        [SerializeField] private bool destroyOnDeath = true;
        [SerializeField] private float destroyDelay = 0.1f;
        [SerializeField] private GameObject deathVisual;
        [SerializeField] private GameObject healthBarRoot;
        [SerializeField] private Collider2D targetCollider;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0;

        public event Action<int, int> HealthChanged;
        public event Action Died;

        private bool _isDeathProcessing;

        private void Awake()
        {
            CurrentHealth = maxHealth;
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

            if (targetCollider != null)
            {
                targetCollider.enabled = false;
            }

            Died?.Invoke();

            if (healthBarRoot != null)
            {
                Destroy(healthBarRoot);
            }

            if (deathVisual != null)
            {
                deathVisual.SetActive(true);
            }

            if (destroyOnDeath)
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

            if (targetCollider == null)
            {
                targetCollider = GetComponent<Collider2D>();
            }
        }
#endif
    }
}