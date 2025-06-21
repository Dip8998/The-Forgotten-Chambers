using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerGroundedState : PlayerState
    {
        protected float moveInput;
        protected float crouchInput;
        private bool jumpInput;
        private bool grabInput;

        public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            player.JumpState.ResetAmountJumpsLeft();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            moveInput = player.InputHandler.MoveInput;
            crouchInput = player.InputHandler.UpInput;
            jumpInput = player.InputHandler.JumpInput;
            grabInput = player.InputHandler.GrabInput;

            if (player.InputHandler.AttackInputs[(int)CombateInputs.Primary])
            {
                stateMachine.ChangeState(player.PrimaryAttackState);
            }
            else if (player.InputHandler.AttackInputs[(int)CombateInputs.Secondary])
            {
                stateMachine.ChangeState(player.SecondaryAttackState);
            }
            else if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!player.CheckIsGround())
            {
                player.AirState.StartCoyoteTime();
                stateMachine.ChangeState(player.AirState);
            }
            else if (player.CheckIsWall() && grabInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
