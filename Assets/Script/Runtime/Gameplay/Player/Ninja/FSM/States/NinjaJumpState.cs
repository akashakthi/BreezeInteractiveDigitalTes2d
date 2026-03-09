using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaJumpState : NinjaState
    {
        private bool _jumpTriggered;

        public NinjaJumpState(NinjaContext context) : base(NinjaStateId.Jump, context)
        {
        }

        public override void Enter()
        {
            _jumpTriggered = false;
            Context.Animator.Play(NinjaAnimationId.Jump, true);
        }

        public override void Update()
        {
            if (!_jumpTriggered)
            {
                Controller.Jump();
                Controller.ConsumeJumpInput();
                _jumpTriggered = true;
            }

            if (Controller.IsAttackPressed())
            {
                Controller.ConsumeAttackInput();
            }

            if (Controller.IsGrounded && Context.Rigidbody.velocity.y <= 0.05f && _jumpTriggered)
            {
                Controller.ChangeState(Controller.HasMoveInput ? NinjaStateId.Run : NinjaStateId.Idle);
            }
        }

        public override void FixedUpdate()
        {
            Controller.MoveHorizontal(Controller.MoveInput);
        }
    }
}