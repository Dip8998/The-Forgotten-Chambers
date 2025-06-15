using ForgottonChambers.StateMachine;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PunchState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public PunchState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {
            playerController.SetPunching(true);

            Debug.Log("Entered PunchState.");
            Debug.Log($"HasSword: {playerController.HasSword}");
            
            if (playerController.HasSword)
                playerController.playerView.PlaySwordAttack();
            else
                playerController.playerView.PlayPunchAnimation();
        }

        public void UpdateState()
        {
            if (!playerController.IsPunching)
            {
                EndPunch();
            }
        }

        private void EndPunch()
        {
            playerController.SetPunching(false);
            if (playerController.IsGrounded())
            {
                float move = playerController.InputHandler.MoveInput;
                playerStateMachine.ChangeState(move != 0 ? PlayerState.Run : PlayerState.Idle);
            }
            else
            {
                playerStateMachine.ChangeState(PlayerState.Jump);
            }
        }

        public void FixedUpdateState()
        {
            float moveInput = playerController.InputHandler.MoveInput;
            playerController.ApplyMovement(moveInput * 0f);
            playerController.SetPlayerScale(moveInput);
        }

        public void OnStateExit()
        {
            playerController.SetPunching(false);
        }
    }
}
