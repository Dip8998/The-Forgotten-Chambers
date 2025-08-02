using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public abstract class PlayerTouchingWallState : PlayerState
    {
        protected float xInput;
        protected float yInput;
        protected bool jumpInput;
        protected bool wallJumpInput;

        public PlayerTouchingWallState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.JumpState.ResetAmountJumpsLeft();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            xInput = Player.InputHandler.MoveInput;
            yInput = Player.InputHandler.UpInput;
            jumpInput = Player.InputHandler.JumpInput;
            wallJumpInput = Player.InputHandler.WallJumpInput;

            if (isExitingState) return;

            if (jumpInput && wallJumpInput)
            {
                Player.AirState.StopWallCoyoteTime();
                Player.WallJumpState.DetermineWallJumpDirection(Player.CheckIsWall());
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else if (Player.CheckIsGround())
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (!Player.CheckIsWall() || (xInput != 0 && xInput != Player.FacingDirection))
            {
                Player.AirState.StartWallCoyoteTime();
                StateMachine.ChangeState(Player.AirState);
            }
        }
    }
}