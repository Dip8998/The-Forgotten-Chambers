using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
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

            player.CheckIfShouldFlip(moveInput);

            player.SetVelocityX(playerData.playerMovementSpeed * moveInput);

            if (!isExitingState)
            {
                if (moveInput == 0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else if (crouchInput == -1)
                {
                    stateMachine.ChangeState(player.CrouchMoveState);
                }
            }
        }
    }
}
