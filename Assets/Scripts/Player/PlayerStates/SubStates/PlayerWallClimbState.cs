using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        public PlayerWallClimbState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.SetVelocityX(0);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.SetVelocityY(PlayerData.playerWallClimbSpeed);

            if (yInput <= 0)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}