using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaRunState : NinjaState
    {
        public NinjaRunState(NinjaContext context) : base(NinjaStateId.Run, context)
        {
        }

        public override void Enter()
        {
            Context.Animator.Play(NinjaAnimationId.Run);
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

            if (!Controller.HasMoveInput)
            {
                Controller.ChangeState(NinjaStateId.Idle);
            }
        }

        public override void FixedUpdate()
        {
            Controller.MoveHorizontal(Controller.MoveInput);
        }
    }
}