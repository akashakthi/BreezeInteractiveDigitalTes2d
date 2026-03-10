using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.CameraSystem
{
    public sealed class PlatformerCameraFollow : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Base Offset")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);

        [Header("Look Ahead")]
        [SerializeField] private float lookAheadDistance = 2.5f;
        [SerializeField] private float lookAheadSmoothTime = 0.15f;

        [Header("Horizontal Follow")]
        [SerializeField] private float horizontalSmoothTime = 0.12f;

        [Header("Vertical Follow")]
        [SerializeField] private float upwardSmoothTime = 0.2f;
        [SerializeField] private float downwardSmoothTime = 0.08f;
        [SerializeField] private float upperDeadZone = 2f;
        [SerializeField] private float lowerDeadZone = 0.5f;

        private float _currentY;
        private float _currentLookAheadX;
        private float _lookAheadVelocity;
        private float _horizontalVelocity;
        private float _verticalVelocity;

        private void Start()
        {
            if (target == null)
            {
                return;
            }

            _currentY = target.position.y + offset.y;

            Vector3 startPosition = transform.position;
            startPosition.x = target.position.x + offset.x;
            startPosition.y = _currentY;
            startPosition.z = offset.z;
            transform.position = startPosition;
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            UpdateHorizontal();
            UpdateVertical();
        }

        private void UpdateHorizontal()
        {
            float facingDirection = Mathf.Sign(target.localScale.x);
            if (Mathf.Approximately(facingDirection, 0f))
            {
                facingDirection = 1f;
            }

            float targetLookAheadX = facingDirection * lookAheadDistance;

            _currentLookAheadX = Mathf.SmoothDamp(
                _currentLookAheadX,
                targetLookAheadX,
                ref _lookAheadVelocity,
                lookAheadSmoothTime
            );

            float desiredX = target.position.x + offset.x + _currentLookAheadX;

            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.SmoothDamp(
                currentPosition.x,
                desiredX,
                ref _horizontalVelocity,
                horizontalSmoothTime
            );

            transform.position = currentPosition;
        }

        private void UpdateVertical()
        {
            float targetY = target.position.y + offset.y;

            float upperLimit = _currentY + upperDeadZone;
            float lowerLimit = _currentY - lowerDeadZone;

            if (targetY > upperLimit)
            {
                float desiredY = targetY - upperDeadZone;
                _currentY = Mathf.SmoothDamp(
                    _currentY,
                    desiredY,
                    ref _verticalVelocity,
                    upwardSmoothTime
                );
            }
            else if (targetY < lowerLimit)
            {
                float desiredY = targetY + lowerDeadZone;
                _currentY = Mathf.SmoothDamp(
                    _currentY,
                    desiredY,
                    ref _verticalVelocity,
                    downwardSmoothTime
                );
            }

            Vector3 currentPosition = transform.position;
            currentPosition.y = _currentY;
            currentPosition.z = offset.z;

            transform.position = currentPosition;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (target == null)
            {
                return;
            }

            float previewY = Application.isPlaying ? _currentY : target.position.y + offset.y;

            Vector3 center = new Vector3(
                target.position.x + offset.x,
                previewY + (upperDeadZone - lowerDeadZone) * 0.5f,
                0f
            );

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(center, new Vector3(3f, upperDeadZone + lowerDeadZone, 0f));
        }
#endif
    }
}