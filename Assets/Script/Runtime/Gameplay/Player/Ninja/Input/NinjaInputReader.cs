using UnityEngine;

namespace BreezeInteractive.Runtime.Gameplay.Player.Ninja.Input
{
    public sealed class NinjaInputReader : MonoBehaviour
    {
        public float MoveAxis { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool AttackPressed { get; private set; }

        private void Update()
        {
            MoveAxis = UnityEngine.Input.GetAxisRaw("Horizontal");

            if (UnityEngine.Input.GetButtonDown("Jump"))
            {
                JumpPressed = true;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.J))
            {
                AttackPressed = true;
            }
        }

        public void ConsumeJump()
        {
            JumpPressed = false;
        }

        public void ConsumeAttack()
        {
            AttackPressed = false;
        }

        public void ClearOneShotInputs()
        {
            JumpPressed = false;
            AttackPressed = false;
        }
    }
}