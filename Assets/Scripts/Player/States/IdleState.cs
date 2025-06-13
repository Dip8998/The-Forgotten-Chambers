using ForgottonChambers.StateMachine;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class IdleState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public IdleState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {
            playerController.playerView.SetPlayerAnimation(0, false, false, false);
        }

        public void UpdateState()
        {
            if (playerController.InputHandler.MoveInput != 0 && playerController.IsGrounded())
            {
                playerStateMachine.ChangeState(PlayerState.Run);
            }
            else if (playerController.InputHandler.JumpInputDown)
            { 
                playerStateMachine.ChangeState(PlayerState.Jump);
            }
            else if (playerController.InputHandler.CrouchInputHeld)
            {
                playerStateMachine.ChangeState(PlayerState.Crouching);
            }
            else if (playerController.InputHandler.PunchInputDown)
            { 
                playerStateMachine.ChangeState(PlayerState.Punch);
            }
        }

        public void FixedUpdateState()
        {
            playerController.ApplyMovement(0);
        }

        public void OnStateExit() { }
    }
}