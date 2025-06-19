using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        public PlayerWallClimbState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            player.SetVelocityY(playerData.playerWallClimbSpeed);

            if(yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState); 
            }
        }
    }
}