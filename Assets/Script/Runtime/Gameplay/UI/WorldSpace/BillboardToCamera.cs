using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.UI.WorldSpace
{
    public sealed class BillboardToCamera : MonoBehaviour
    {
        [Header("Follow")]
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);
        [SerializeField] private bool followTarget = true;

        [Header("Camera Facing")]
        [SerializeField] private Camera targetCamera;
        [SerializeField] private bool faceCamera = false;
        [SerializeField] private bool lockX = true;
        [SerializeField] private bool lockY = true;
        [SerializeField] private bool lockZ = false;

        [Header("Scale Protection")]
        [SerializeField] private bool keepPositiveScale = true;

        private Vector3 _initialScale;

        private void Awake()
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            _initialScale = transform.localScale;
        }

        private void LateUpdate()
        {
            if (followTarget && target != null)
            {
                transform.position = target.position + worldOffset;
            }

            if (faceCamera && targetCamera != null)
            {
                Vector3 euler = targetCamera.transform.rotation.eulerAngles;

                if (lockX)
                {
                    euler.x = 0f;
                }

                if (lockY)
                {
                    euler.y = 0f;
                }

                if (lockZ)
                {
                    euler.z = 0f;
                }

                transform.rotation = Quaternion.Euler(euler);
            }
            else
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
    }
}