using UnityEngine;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Input;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Movement;
using BreezeInteractive.Runtime.Gameplay.Player.Ninja.Combat;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Core
{
    public sealed class NinjaContext
    {
        public NinjaController Controller { get; }
        public NinjaAnimator Animator { get; }
        public NinjaInputReader Input { get; }
        public GroundChecker GroundChecker { get; }
        public NinjaHealth Health { get; }
        public Rigidbody2D Rigidbody { get; }
        public Transform Transform { get; }

        public NinjaContext(
            NinjaController controller,
            NinjaAnimator animator,
            NinjaInputReader input,
            GroundChecker groundChecker,
            NinjaHealth health,
            Rigidbody2D rigidbody,
            Transform transform)
        {
            Controller = controller;
            Animator = animator;
            Input = input;
            GroundChecker = groundChecker;
            Health = health;
            Rigidbody = rigidbody;
            Transform = transform;
        }
    }
}