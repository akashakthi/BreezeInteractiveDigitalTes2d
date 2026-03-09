using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaHurtState : NinjaState
    {
        private float _timer;

        public NinjaHurtState(NinjaContext context) : base(NinjaStateId.Hurt, context)
        {
        }

        public override void Enter()
        {
            _timer = 0f;
            Context.Animator.Play(NinjaAnimationId.Hurt, true);
        }

        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= Controller.HurtDuration)
            {
                if (Controller.IsDead)
                {
                    Controller.ChangeState(NinjaStateId.Die);
                    return;
                }

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