using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerInAirState : PlayerState
    {
        private float xInput;
        private bool jumpInput;
        private bool coyoteTime;

        public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
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

            CheckCoyoteTime();

            xInput = player.InputHandler.MoveInput;
            jumpInput = player.InputHandler.JumpInput;

            if (player.CheckIsGround() && player.CurrentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if(jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState); 
            }
            else
            {
                player.SetVelocityX(playerData.playerMovementSpeed * xInput);
            }
        }

        private void CheckCoyoteTime()
        {
            if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
            {
                coyoteTime = false;
                player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public void StartCoyoteTime() => coyoteTime = true; 
    }
}
