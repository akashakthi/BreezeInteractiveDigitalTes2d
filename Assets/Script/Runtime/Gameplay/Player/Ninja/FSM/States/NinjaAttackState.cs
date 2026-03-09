using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaAttackState : NinjaState
    {
        private float _timer;
        private bool _hasAppliedDamage;

        public NinjaAttackState(NinjaContext context) : base(NinjaStateId.Attack, context)
        {
        }

        public override void Enter()
        {
            _timer = 0f;
            _hasAppliedDamage = false;

            Controller.ConsumeAttackInput();
            Context.Animator.Play(NinjaAnimationId.Attack, true);
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (!_hasAppliedDamage && _timer >= Controller.AttackHitTime)
            {
                _hasAppliedDamage = true;
                Context.AttackHitbox.PerformAttack();
            }

            if (_timer >= Controller.AttackDuration)
            {
                if (!Controller.IsGrounded)
                {
                    Controller.ChangeState(NinjaStateId.Jump);
                    return;
                }

                Controller.ChangeState(Controller.HasMoveInput ? NinjaStateId.Run : NinjaStateId.Idle);
            }
        }

        public override void FixedUpdate()
        {
            Controller.StopHorizontal();
        }
    }
}