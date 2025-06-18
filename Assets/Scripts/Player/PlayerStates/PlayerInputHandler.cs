using UnityEngine;

namespace ForgottonChambers.Player
{
    using UnityEngine;

    namespace ForgottonChambers.Player
    {
        public class PlayerInputHandler
        {
            public float MoveInput { get; private set; }

            public void UpdateInputs()
            {
                MoveInput = Input.GetAxisRaw("Horizontal");
            }
        }
    }
}