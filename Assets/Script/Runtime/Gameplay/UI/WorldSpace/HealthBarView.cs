using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Common.Combat;

namespace BreezeInteractive.Runtime.Gameplay.UI.WorldSpace
{
    public sealed class HealthBarView : MonoBehaviour
    {
        [Header("Source")]
        [SerializeField] private Transform owner;

        [Header("Fill")]
        [SerializeField] private SpriteRenderer fillRenderer;

        private IHealthSource _healthSource;
        private float _initialWidth;
        private Vector3 _initialFillLocalPosition;
        private bool _isInitialized;

        private void Awake()
        {
            if (fillRenderer != null)
            {
                _initialWidth = fillRenderer.size.x;
                _initialFillLocalPosition = fillRenderer.transform.localPosition;
            }
        }

        private void Start()
        {
            ResolveSource();
            RefreshImmediate();
        }

        private void OnEnable()
        {
            ResolveSource();

            if (_healthSource != null)
            {
                _healthSource.HealthChanged += HandleHealthChanged;
            }
        }

        private void OnDisable()
        {
            if (_healthSource != null)
            {
                _healthSource.HealthChanged -= HandleHealthChanged;
            }
        }

        private void ResolveSource()
        {
            if (_isInitialized)
            {
                return;
            }

            if (owner == null)
            {
                Debug.LogError($"{name}: Owner is not assigned.", this);
                return;
            }

            _healthSource = owner.GetComponent<IHealthSource>();

            if (_healthSource == null)
            {
                Debug.LogError($"{name}: Owner does not contain a component that implements IHealthSource.", this);
                return;
            }

            _isInitialized = true;
        }

        private void RefreshImmediate()
        {
            if (_healthSource == null)
            {
                return;
            }

            HandleHealthChanged(_healthSource.CurrentHealth, _healthSource.MaxHealth);
        }

        private void HandleHealthChanged(int current, int max)
        {
            UpdateFill(current, max);
        }

        private void UpdateFill(int current, int max)
        {
            if (fillRenderer == null)
            {
                return;
            }

            float normalized = max <= 0 ? 0f : Mathf.Clamp01((float)current / max);
            float newWidth = _initialWidth * normalized;

            fillRenderer.size = new Vector2(newWidth, fillRenderer.size.y);

            float offsetX = (_initialWidth - newWidth) * 0.5f;
            fillRenderer.transform.localPosition = new Vector3(
                _initialFillLocalPosition.x - offsetX,
                _initialFillLocalPosition.y,
                _initialFillLocalPosition.z
            );

            fillRenderer.enabled = newWidth > 0f;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (fillRenderer == null)
            {
                fillRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }
#endif
    }
}