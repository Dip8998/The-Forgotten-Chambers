using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public PlayerWallSlideState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            player.SetVelocityY(-playerData.playerWallSlideSpeed);

            if(grabInput && yInput == 0)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}
