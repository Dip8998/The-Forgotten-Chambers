using ForgottonChambers.StateMachine;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PunchState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;
        private bool animationTriggered = false;

        public PunchState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {
            animationTriggered = true;

            playerController.playerView.PlayPunchAnimation();
            playerController.SetPunching(true);
        }

        public void UpdateState()
        {
            if (animationTriggered && !playerController.IsPunching)
            {
                animationTriggered = false;

                if (playerController.IsGrounded())
                {
                    float moveInput = playerController.InputHandler.MoveInput;
                    playerStateMachine.ChangeState(moveInput != 0 ? PlayerState.Run : PlayerState.Idle);
                }
                else
                {
                    playerStateMachine.ChangeState(PlayerState.Jump);
                }
            }
        }

        public void FixedUpdateState()
        {
            float horizontalInput = playerController.InputHandler.MoveInput;
            playerController.ApplyMovement(horizontalInput * 0.2f);
            playerController.SetPlayerScale(horizontalInput);
        }

        public void OnStateExit()
        {
            playerController.SetPunching(false);
            animationTriggered = false;
        }
    }
}
