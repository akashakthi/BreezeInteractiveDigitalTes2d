using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.UI.WorldSpace
{
    public sealed class WorldSpaceFollower : MonoBehaviour
    {
        [Header("Follow")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);
        [SerializeField] private bool followPosition = true;

        [Header("Rotation")]
        [SerializeField] private bool keepWorldRotation = true;

        [Header("Scale")]
        [SerializeField] private bool keepPositiveScale = true;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void LateUpdate()
        {
            if (followPosition && target != null)
            {
                transform.position = target.position + worldOffset;
            }

            if (keepWorldRotation)
            {
                transform.rotation = Quaternion.identity;
            }

            if (keepPositiveScale)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(_initialScale.x);
                scale.y = Mathf.Abs(_initialScale.y);
                scale.z = Mathf.Abs(_initialScale.z);
                transform.localScale = scale;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (transform.localScale.x < 0f ||
                transform.localScale.y < 0f ||
                transform.localScale.z < 0f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                scale.y = Mathf.Abs(scale.y);
                scale.z = Mathf.Abs(scale.z);
                transform.localScale = scale;
            }
        }
#endif
    }
}