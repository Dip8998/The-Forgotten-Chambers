using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerWallJumpState : PlayerAbilityState
    {
        private int _wallJumpDir;

        public PlayerWallJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Player.JumpState.ResetAmountJumpsLeft();
            Player.SetVelocity(PlayerData.playerWallJumpSpeed, PlayerData.playerWallJumpAngle, _wallJumpDir);
            Player.CheckIfShouldFlip(_wallJumpDir);
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.PlayerView.PlayerAnimator.SetFloat("yVelocity", Player.CurrentVelocity.y);
            Player.PlayerView.PlayerAnimator.SetFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));

            if (Time.time >= startTime + PlayerData.playerWallJumpTime)
            {
                isAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            _wallJumpDir = isTouchingWall ? -Player.FacingDirection : Player.FacingDirection;
        }
    }
}