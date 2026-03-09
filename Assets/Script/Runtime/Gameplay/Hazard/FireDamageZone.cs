using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Common.Combat;

namespace BreezeInteractive.Runtime.Gameplay.Hazard
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class FireDamageZone : MonoBehaviour
    {
        [SerializeField] private int damagePerTick = 20;
        [SerializeField] private float tickInterval = 1f;

        private readonly Dictionary<IDamageable, Coroutine> _activeDamageRoutines = new();

        private void Reset()
        {
            Collider2D col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                return;
            }

            if (_activeDamageRoutines.ContainsKey(damageable))
            {
                return;
            }

            Coroutine routine = StartCoroutine(DamageOverTimeRoutine(damageable));
            _activeDamageRoutines.Add(damageable, routine);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                return;
            }

            StopDamageRoutine(damageable);
        }

        private IEnumerator DamageOverTimeRoutine(IDamageable damageable)
        {
            while (damageable != null)
            {
                damageable.TakeDamage(damagePerTick);
                yield return new WaitForSeconds(tickInterval);
            }
        }

        private void StopDamageRoutine(IDamageable damageable)
        {
            if (!_activeDamageRoutines.TryGetValue(damageable, out Coroutine routine))
            {
                return;
            }

            if (routine != null)
            {
                StopCoroutine(routine);
            }

            _activeDamageRoutines.Remove(damageable);
        }

        private void OnDisable()
        {
            foreach (Coroutine routine in _activeDamageRoutines.Values)
            {
                if (routine != null)
                {
                    StopCoroutine(routine);
                }
            }

            _activeDamageRoutines.Clear();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (damagePerTick < 0)
            {
                damagePerTick = 0;
            }

            if (tickInterval <= 0f)
            {
                tickInterval = 0.1f;
            }
        }
#endif
    }
}