using ForgottonChambers.StateMachine;

namespace ForgottonChambers.Player
{
    public class CrouchWalkState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public CrouchWalkState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter() { }

        public void UpdateState()
        {
            if (!playerController.InputHandler.CrouchInputHeld)
            {
                playerStateMachine.ChangeState(PlayerState.Run);
            }
            else if (playerController.InputHandler.MoveInput == 0)
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
            float horizontalInput = playerController.InputHandler.MoveInput;
            playerController.ApplyMovement(horizontalInput * playerController.playerScriptableObject.playerCrouchMovementSpeed);
            playerController.playerView.SetPlayerAnimation(horizontalInput, false, true, false);
            playerController.SetPlayerScale(horizontalInput);
        }

        public void OnStateExit() { }
    }
}