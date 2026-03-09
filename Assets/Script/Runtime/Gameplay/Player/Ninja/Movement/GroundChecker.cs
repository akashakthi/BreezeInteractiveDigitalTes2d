using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Movement
{
    public sealed class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform checkPoint;
        [SerializeField] private float radius = 0.15f;
        [SerializeField] private LayerMask groundLayer;

        public bool IsGrounded()
        {
            if (checkPoint == null)
            {
                return false;
            }

            return Physics2D.OverlapCircle(checkPoint.position, radius, groundLayer) != null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (checkPoint == null)
            {
                return;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(checkPoint.position, radius);
        }
#endif
    }
}