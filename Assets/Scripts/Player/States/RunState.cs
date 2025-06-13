using ForgottonChambers.StateMachine;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class RunState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public RunState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {

        }

        public void UpdateState()
        {
            if (playerController.InputHandler.MoveInput == 0 && playerController.IsGrounded())
            {
                playerStateMachine.ChangeState(PlayerState.Idle);
            }
            else if (playerController.InputHandler.JumpInputDown)
            {
                playerStateMachine.ChangeState(PlayerState.Jump);
            }
            else if (playerController.InputHandler.CrouchInputHeld)
            {
                playerStateMachine.ChangeState(PlayerState.CrouchWalk);
            }
            else if (playerController.InputHandler.PunchInputDown)
            {
                playerStateMachine.ChangeState(PlayerState.Punch);
            }
        }

        public void FixedUpdateState()
        {
            float horizontalInput = playerController.InputHandler.MoveInput;
            playerController.ApplyMovement(horizontalInput);
            playerController.playerView.SetPlayerAnimation(horizontalInput, false, false, false);
            playerController.SetPlayerScale(horizontalInput);

        }

        public void OnStateExit() { }
    }
}