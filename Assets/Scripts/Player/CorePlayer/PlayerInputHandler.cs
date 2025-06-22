using System;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public enum CombateInputs
    {
        Primary,
        Secondary,
        Count
    }

    public class PlayerInputHandler
    {
        public float MoveInput { get; private set; }
        public float UpInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool GrabInput { get; private set; }
        public bool BoxPushPullInput { get; private set; }
        public bool BoxDropInput { get; private set; }
        public bool[] AttackInputs { get; private set; }

        public PlayerInputHandler()
        {
            AttackInputs = new bool[(int)CombateInputs.Count];
        }

        public void StartInputs()
        {
        }

        public void UpdateInputs()
        {
            MoveInput = Input.GetAxisRaw("Horizontal");
            UpInput = Input.GetAxisRaw("Vertical");
            JumpInput = Input.GetKeyDown(KeyCode.Space);
            GrabInput = Input.GetKey(KeyCode.LeftShift);
            BoxPushPullInput = Input.GetKey(KeyCode.E);
            BoxDropInput = Input.GetKeyUp(KeyCode.E);
            AttackInputs[(int)CombateInputs.Primary] = Input.GetMouseButton(0);
            AttackInputs[(int)CombateInputs.Secondary] = Input.GetMouseButton(1);
        }
    }
}