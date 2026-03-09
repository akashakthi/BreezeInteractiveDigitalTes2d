using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaIdleState : NinjaState
    {
        public NinjaIdleState(NinjaContext context) : base(NinjaStateId.Idle, context)
        {
        }

        public override void Enter()
        {
            Context.Animator.Play(NinjaAnimationId.Idle);
        }

        public override void HandleInput()
        {
            if (Controller.IsDead)
            {
                Controller.ChangeState(NinjaStateId.Die);
                return;
            }

            if (Controller.IsAttackPressed())
            {
                Controller.ChangeState(NinjaStateId.Attack);
                return;
            }

            if (Controller.IsJumpPressed() && Controller.IsGrounded)
            {
                Controller.ChangeState(NinjaStateId.Jump);
                return;
            }

            if (Controller.HasMoveInput)
            {
                Controller.ChangeState(NinjaStateId.Run);
            }
        }

        public override void FixedUpdate()
        {
            Controller.StopHorizontal();
        }
    }
}