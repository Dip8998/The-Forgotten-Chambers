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

        public bool PunchInputHeld { get; private set; }

        public void UpdateInputs()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            CrouchInputHeld = Input.GetKey(KeyCode.DownArrow);
            PunchInputDown = Input.GetMouseButtonDown(0);
            PunchInputHeld = Input.GetMouseButton(0);

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