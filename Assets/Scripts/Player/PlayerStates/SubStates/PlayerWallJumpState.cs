using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerWallJumpState : PlayerAbilityState
    {
        private int wallJumpDir;

        public PlayerWallJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            player.JumpState.ResetAmountJumpsLeft();
            player.SetVelocity(playerData.playerWallJumpSpeed, playerData.playerWallJumpAngle, wallJumpDir);
            player.CheckIfShouldFlip(wallJumpDir);
            player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if(Time.time >= startTime + playerData.playerWallJumpTime)
            {
                isAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                wallJumpDir = -player.FacingDirection;
            }
            else
            {
                wallJumpDir = player.FacingDirection;
            }
        }
    }
}
