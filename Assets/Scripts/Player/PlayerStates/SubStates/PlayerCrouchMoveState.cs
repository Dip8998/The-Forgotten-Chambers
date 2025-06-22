using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerCrouchMoveState : PlayerGroundedState
    {
        public PlayerCrouchMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.SetColliderSize(PlayerData.playerCrouchColliderSize, PlayerData.playerCrouchColliderOffset);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Player.SetColliderSize(PlayerData.playerStandColliderSize, PlayerData.playerStandColliderOffset);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.SetVelocityX(PlayerData.playerCrouchMovementSpeed * Player.FacingDirection);
            Player.CheckIfShouldFlip(moveInput);

            if (moveInput == 0)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
            else if (verticalInput != -1 && !Player.CheckIsCeiling())
            {
                StateMachine.ChangeState(Player.MoveState);
            }
        }
    }
}