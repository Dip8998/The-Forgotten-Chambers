using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerInputHandler
    {
        public float MoveInput { get; private set; }
        public bool JumpInputDown { get; private set; } 
        public bool CrouchInputHeld { get; private set; }
        public bool PunchInputDown { get; private set; }

        private float jumpBufferTime = 0.1f;
        private float jumpBufferCounter = 0f;

        public void UpdateInputs()
        {
            MoveInput = UnityEngine.Input.GetAxisRaw("Horizontal");
            CrouchInputHeld = UnityEngine.Input.GetKey(KeyCode.DownArrow);
            PunchInputDown = UnityEngine.Input.GetMouseButtonDown(0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }
            JumpInputDown = (jumpBufferCounter > 0);
        }

        public void ResetJumpBuffer()
        {
            jumpBufferCounter = 0f;
        }
    }
}