using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerInAirState : PlayerState
    {
        private float xInput;
        private bool jumpInput;
        private bool grabInput;
        private bool coyoteTime;
        private bool wallJumpCoyoteTime;
        private float startWallJumpCoyoteTime;

        public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            CheckCoyoteTime();
            CheckWallCoyoteTime();

            xInput = player.InputHandler.MoveInput;
            jumpInput = player.InputHandler.JumpInput;
            grabInput = player.InputHandler.GrabInput;

            if (player.InputHandler.AttackInputs[(int)CombateInputs.Primary] && !player.CheckIsCeiling())
            {
                stateMachine.ChangeState(player.PrimaryAttackState);
            }
            else if (player.InputHandler.AttackInputs[(int)CombateInputs.Secondary] && !player.CheckIsCeiling())
            {
                stateMachine.ChangeState(player.SecondaryAttackState);
            }
            else if (player.CheckIsGround() && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if(jumpInput && (player.CheckIsWall() || player.CheckIsWallBack() || wallJumpCoyoteTime))
            {
                StopWallCoyoteTime();
                player.WallJumpState.DetermineWallJumpDirection(player.CheckIsWall());
                stateMachine.ChangeState(player.WallJumpState);
            }
            else if (jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (player.CheckIsWall() && grabInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (player.CheckIsWall() && xInput == player.FacingDirection && player.CurrentVelocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else
            {
                player.CheckIfShouldFlip(xInput);
                player.SetVelocityX(playerData.playerMovementSpeed * xInput);

                player.playerView.playerAnimator.SetFloat("yVelocity", player.CurrentVelocity.y);
                player.playerView.playerAnimator.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
            }
        }

        private void CheckCoyoteTime()
        {
            if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
            {
                coyoteTime = false;
                player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        private void CheckWallCoyoteTime()
        {
            if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.coyoteTime)
            {
                wallJumpCoyoteTime = false;
                player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public void StartCoyoteTime() => coyoteTime = true; 

        public void StartWallCoyoteTime()
        {
            wallJumpCoyoteTime = true;
            startWallJumpCoyoteTime = Time.time;
        }

        public void StopWallCoyoteTime() => wallJumpCoyoteTime = false;

    }
}
