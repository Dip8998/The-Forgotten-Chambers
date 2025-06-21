using UnityEngine;

namespace ForgottonChambers.Player
{
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
            public bool BoxDropeInput { get; private set; }
            public bool[] AttackInputs { get; private set; }

            public void StartInputs()
            {
                int count = Enum.GetValues(typeof(CombateInputs)).Length;
                AttackInputs = new bool[count];
            }

            public void UpdateInputs()
            {
                MoveInput = Input.GetAxisRaw("Horizontal");
                UpInput = Input.GetAxisRaw("Vertical");
                JumpInput = Input.GetKeyDown(KeyCode.Space);
                GrabInput = Input.GetKey(KeyCode.LeftShift);
                BoxPushPullInput = Input.GetKey(KeyCode.E);
                BoxDropeInput = Input.GetKeyUp(KeyCode.E);
                AttackInputs[(int)CombateInputs.Primary] = Input.GetMouseButton(0);
                AttackInputs[(int)CombateInputs.Secondary] = Input.GetMouseButton(1);
            }
        }
    }

    public enum CombateInputs
    {
        Primary,
        Secondary
    }
}