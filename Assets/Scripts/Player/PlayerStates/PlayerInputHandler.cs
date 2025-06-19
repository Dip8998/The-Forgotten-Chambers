using UnityEngine;

namespace ForgottonChambers.Player
{
    using UnityEngine;

    namespace ForgottonChambers.Player
    {
        public class PlayerInputHandler
        {
            public float MoveInput { get; private set; }
            public float UpInput { get; private set; }
            public bool JumpInput { get; private set; }
            public bool GrabInput { get; private set; }

            public void UpdateInputs()
            {
                MoveInput = Input.GetAxisRaw("Horizontal");
                UpInput = Input.GetAxisRaw("Vertical");
                JumpInput = Input.GetKeyDown(KeyCode.Space);
                GrabInput = Input.GetKey(KeyCode.LeftShift);
            }
        }
    }
}