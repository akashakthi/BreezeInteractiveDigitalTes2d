using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM
{
    public abstract class NinjaState
    {
        protected NinjaContext Context { get; }
        protected NinjaController Controller => Context.Controller;

        public NinjaStateId StateId { get; }

        protected NinjaState(NinjaStateId stateId, NinjaContext context)
        {
            StateId = stateId;
            Context = context;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void HandleInput() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}