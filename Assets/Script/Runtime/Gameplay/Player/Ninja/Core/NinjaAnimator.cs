using System.Collections.Generic;
using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core
{
    public sealed class NinjaAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float crossFadeDuration = 0.05f;

        private readonly Dictionary<NinjaAnimationId, int> _stateHashes = new();

        private NinjaAnimationId? _currentAnimation;

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            CacheHashes();
        }

        private void CacheHashes()
        {
            _stateHashes[NinjaAnimationId.Idle] = Animator.StringToHash("NinjaIdle");
            _stateHashes[NinjaAnimationId.Run] = Animator.StringToHash("NinjaRun");
            _stateHashes[NinjaAnimationId.Jump] = Animator.StringToHash("NinjaJump");
            _stateHashes[NinjaAnimationId.Attack] = Animator.StringToHash("NinjaAttack");
            _stateHashes[NinjaAnimationId.Hurt] = Animator.StringToHash("NinjaHurt");
            _stateHashes[NinjaAnimationId.Die] = Animator.StringToHash("NinjaDie");
        }

        public void Play(NinjaAnimationId animationId, bool forceReplay = false)
        {
            if (animator == null)
            {
                return;
            }

            if (!forceReplay && _currentAnimation.HasValue && _currentAnimation.Value == animationId)
            {
                return;
            }

            if (_stateHashes.TryGetValue(animationId, out int hash))
            {
                animator.CrossFadeInFixedTime(hash, crossFadeDuration);
                _currentAnimation = animationId;
            }
        }
    }
}