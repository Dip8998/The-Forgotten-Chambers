using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.CheckIfShouldFlip(moveInput);
            Player.SetVelocityX(PlayerData.playerMovementSpeed * moveInput);

            if (moveInput == 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (verticalInput == -1)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
        }
    }
}