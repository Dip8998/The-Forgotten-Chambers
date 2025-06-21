using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerCrouchMoveState : PlayerGroundedState
    {
        public PlayerCrouchMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            player.SetColliderSize(
                               new Vector2(1.0f, 0.5f),
                               new Vector2(0.0f, -0.25f));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            player.SetColliderSize(
                           new Vector2(1.0f, 1.8f),
                           new Vector2(0.0f, 0.0f));
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!isExitingState)
            {
                player.SetVelocityX(playerData.playerCrouchMovementSpeed * player.FacingDirection);
                player.CheckIfShouldFlip(moveInput);

                if(moveInput == 0)
                {
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
                else if(crouchInput != -1 && !player.CheckIsCeiling())
                {
                    stateMachine.ChangeState(player.MoveState);
                }
            }
        }
    }
}