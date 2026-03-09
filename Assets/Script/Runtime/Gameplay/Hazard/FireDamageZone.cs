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

        private readonly Dictionary<IDamageable, float> _timers = new Dictionary<IDamageable, float>();

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

            if (!_timers.ContainsKey(damageable))
            {
                _timers.Add(damageable, 0f);
                damageable.TakeDamage(damagePerTick);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                return;
            }

            if (!_timers.ContainsKey(damageable))
            {
                _timers.Add(damageable, 0f);
            }

            _timers[damageable] += Time.deltaTime;

            if (_timers[damageable] >= tickInterval)
            {
                _timers[damageable] = 0f;
                damageable.TakeDamage(damagePerTick);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable == null)
            {
                return;
            }

            if (_timers.ContainsKey(damageable))
            {
                _timers.Remove(damageable);
            }
        }
    }
}