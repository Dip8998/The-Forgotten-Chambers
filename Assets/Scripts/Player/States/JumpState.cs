using ForgottonChambers.Player;
using ForgottonChambers.StateMachine;
using UnityEngine;

public class JumpState : IState
{
    private PlayerController playerController;
    private PlayerStateMachine playerStateMachine;
    private float groundedTimeBuffer;

    public JumpState(PlayerController playerController, PlayerStateMachine playerStateMachine)
    {
        this.playerController = playerController;
        this.playerStateMachine = playerStateMachine;
    }

    public void OnStateEnter()
    {
        playerController.ApplyJumpForce(playerController.playerScriptableObject.playerJumpForce);
        playerController.playerView.SetPlayerAnimation(playerController.InputHandler.MoveInput, true, false, false);
        playerController.ResetDoubleJumpAbility();
        groundedTimeBuffer = Time.time;
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerController.CanDoubleJump)
        {
            playerStateMachine.ChangeState(PlayerState.DoubleJump);
        }
        else if (playerController.IsGrounded())
        {
            if (Time.time - groundedTimeBuffer > 0.1f)
            {
                playerController.playerView.SetPlayerAnimation(playerController.InputHandler.MoveInput, false, false, false);
                playerStateMachine.ChangeState(PlayerState.Idle);
            }
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
        playerController.SetPlayerScale(horizontalInput);
    }

    public void OnStateExit()
    {
   
    }
}
