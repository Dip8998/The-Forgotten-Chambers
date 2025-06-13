using ForgottonChambers.StateMachine;

namespace ForgottonChambers.Player
{
    public class CrouchingState : IState
    {
        private PlayerController playerController;
        private PlayerStateMachine playerStateMachine;

        public CrouchingState(PlayerController playerController, PlayerStateMachine playerStateMachine)
        {
            this.playerController = playerController;
            this.playerStateMachine = playerStateMachine;
        }

        public void OnStateEnter()
        {
            playerController.playerView.SetPlayerAnimation(0, false, false, true);
        }

        public void UpdateState()
        {
            if (!playerController.InputHandler.CrouchInputHeld)
            {
                playerStateMachine.ChangeState(PlayerState.Idle);
            }
            else if (playerController.InputHandler.MoveInput != 0)
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
            playerController.ApplyMovement(0);
        }

        public void OnStateExit() { }
    }
}