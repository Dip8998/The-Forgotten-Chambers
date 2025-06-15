using ForgottonChambers.StateMachine;

namespace ForgottonChambers.Player
{
    public class DoubleJumpState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public DoubleJumpState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {
            playerController.ApplyJumpForce(playerController.playerScriptableObject.playerDoubleJumpForce);
            playerController.playerView.PlayAirSpinAnimation();
            playerController.DisableDoubleJumpAbility();
        }

        public void UpdateState()
        {
            if (playerController.IsGrounded())
            {
                playerStateMachine.ChangeState(PlayerState.Idle);
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
            playerController.playerView.SetPlayerAnimation(horizontalInput, !playerController.IsGrounded(), false, false);
            playerController.SetPlayerScale(horizontalInput);
        }

        public void OnStateExit() { }
    }
}