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

            bool keyboardJump = UnityEngine.Input.GetButtonDown("Jump");
            bool gamepadJump = UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton1); // PS X

            if (keyboardJump || gamepadJump)
            {
                JumpPressed = true;
            }

            bool keyboardAttack = UnityEngine.Input.GetKeyDown(KeyCode.J);
            bool gamepadAttack = UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton0); // PS Square

            if (keyboardAttack || gamepadAttack)
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