using System.Collections.Generic;
using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Common.Combat;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat
{
    public sealed class NinjaAttackHitbox : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private int attackDamage = 50;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRadius = 0.6f;
        [SerializeField] private LayerMask targetLayers;

        private readonly Collider2D[] _results = new Collider2D[16];

        public int AttackDamage => attackDamage;

        public void PerformAttack()
        {
            if (attackPoint == null)
            {
                Debug.LogWarning($"{nameof(NinjaAttackHitbox)}: Attack point is not assigned.");
                return;
            }

            int count = Physics2D.OverlapCircleNonAlloc(
                attackPoint.position,
                attackRadius,
                _results,
                targetLayers);

            if (count <= 0)
            {
                return;
            }

            HashSet<IDamageable> hitTargets = new HashSet<IDamageable>();

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = _results[i];
                if (hit == null)
                {
                    continue;
                }

                IDamageable damageable = hit.GetComponentInParent<IDamageable>();
                if (damageable == null)
                {
                    continue;
                }

                if (damageable.GetTransform() == transform.root)
                {
                    continue;
                }

                if (hitTargets.Add(damageable))
                {
                    damageable.TakeDamage(attackDamage);
                }

                _results[i] = null;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        private void OnValidate()
        {
            if (attackRadius < 0f)
            {
                attackRadius = 0f;
            }

            if (attackDamage < 0)
            {
                attackDamage = 0;
            }
        }
#endif
    }
}