using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Data;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM.States
{
    public sealed class NinjaDieState : NinjaState
    {
        public NinjaDieState(NinjaContext context) : base(NinjaStateId.Die, context)
        {
        }

        public override void Enter()
        {
            Context.Animator.Play(NinjaAnimationId.Die, true);
        }

        public override void HandleInput()
        {
            // Intentionally empty. Dead state locks control.
        }

        public override void FixedUpdate()
        {
            Controller.StopHorizontal();
        }
    }
}