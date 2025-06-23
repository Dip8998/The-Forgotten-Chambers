using System;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerInputHandler
    {
        public float MoveInput { get; private set; }
        public float UpInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool GrabInput { get; private set; }
        public bool BoxPushPullInput { get; private set; }
        public bool BoxDropInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool SwitchWeaponInput { get; private set; }

        public void UpdateInputs()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            UpInput = Input.GetAxisRaw("Vertical");
            JumpInput = Input.GetKeyDown(KeyCode.Space);
            GrabInput = Input.GetKey(KeyCode.LeftShift);
            BoxPushPullInput = Input.GetKey(KeyCode.E);
            BoxDropInput = Input.GetKeyUp(KeyCode.E);
            AttackInput= Input.GetMouseButton(0);
            SwitchWeaponInput = Input.GetKeyDown(KeyCode.Tab);
        }
    }
}