using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat;
using BreezeInteractive.Runtime.Gameplay.Enemy.Dummy;

namespace BreezeInteractive.Runtime.Gameplay.UI.WorldSpace
{
    public sealed class HealthBarView : MonoBehaviour
    {
        [Header("Fill")]
        [SerializeField] private SpriteRenderer fillRenderer;

        [Header("Optional Sources")]
        [SerializeField] private NinjaHealth ninjaHealth;
        [SerializeField] private DummyTarget dummyTarget;

        private float _initialWidth;
        private Vector3 _initialFillLocalPosition;

        private void Awake()
        {
            if (fillRenderer != null)
            {
                _initialWidth = fillRenderer.size.x;
                _initialFillLocalPosition = fillRenderer.transform.localPosition;
            }
        }

        private void OnEnable()
        {
            if (ninjaHealth != null)
            {
                ninjaHealth.HealthChanged += HandleNinjaHealthChanged;
                HandleNinjaHealthChanged(ninjaHealth.CurrentHealth, ninjaHealth.MaxHealth);
            }

            if (dummyTarget != null)
            {
                dummyTarget.HealthChanged += HandleDummyHealthChanged;
                HandleDummyHealthChanged(dummyTarget.CurrentHealth, dummyTarget.MaxHealth);
            }
        }

        private void OnDisable()
        {
            if (ninjaHealth != null)
            {
                ninjaHealth.HealthChanged -= HandleNinjaHealthChanged;
            }

            if (dummyTarget != null)
            {
                dummyTarget.HealthChanged -= HandleDummyHealthChanged;
            }
        }

        private void HandleNinjaHealthChanged(int current, int max)
        {
            UpdateFill(current, max);
        }

        private void HandleDummyHealthChanged(int current, int max)
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
        }
    }
}