using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public PlayerWallSlideState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.SetVelocityY(-PlayerData.playerWallSlideSpeed);

            if (grabInput && yInput == 0)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}