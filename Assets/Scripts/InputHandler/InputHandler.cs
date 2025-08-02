using System;
using UnityEngine;

namespace ForgottonChambers.Inputs
{
    public class InputHandler
    {
        public float MoveInput { get; private set; }
        public float UpInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool WallJumpInput { get; private set; }
        public bool BoxPushPullInput { get; private set; }
        public bool BoxDropInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool SwitchWeaponInput { get; private set; }
        public bool PauseInput { get; private set; }

        public void UpdateInputs()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            UpInput = Input.GetAxisRaw("Vertical");
            JumpInput = Input.GetKeyDown(KeyCode.Space);
            WallJumpInput = Input.GetKey(KeyCode.LeftShift);
            BoxPushPullInput = Input.GetKey(KeyCode.E);
            BoxDropInput = Input.GetKeyUp(KeyCode.E);
            AttackInput= Input.GetMouseButton(0);
            SwitchWeaponInput = Input.GetKeyDown(KeyCode.Tab);
            PauseInput = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}