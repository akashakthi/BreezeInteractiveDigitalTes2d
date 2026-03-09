using System.Collections.Generic;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.FSM
{
    public sealed class NinjaStateMachine
    {
        private readonly Dictionary<NinjaStateId, NinjaState> _states = new();

        public NinjaState CurrentState { get; private set; }

        public void Register(NinjaState state)
        {
            if (state == null)
            {
                return;
            }

            if (_states.ContainsKey(state.StateId))
            {
                _states[state.StateId] = state;
                return;
            }

            _states.Add(state.StateId, state);
        }

        public void ChangeState(NinjaStateId nextStateId)
        {
            if (!_states.TryGetValue(nextStateId, out NinjaState nextState))
            {
                return;
            }

            if (CurrentState != null && CurrentState.StateId == nextStateId)
            {
                return;
            }

            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }

        public void HandleInput()
        {
            CurrentState?.HandleInput();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}