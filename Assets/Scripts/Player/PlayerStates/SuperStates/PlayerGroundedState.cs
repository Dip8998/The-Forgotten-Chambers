using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public abstract class PlayerGroundedState : PlayerState
    {
        protected float moveInput;
        protected float verticalInput;
        private bool jumpInput;

        public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.JumpState.ResetAmountJumpsLeft();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            moveInput = Player.InputHandler.MoveInput;
            verticalInput = Player.InputHandler.UpInput;
            jumpInput = Player.InputHandler.JumpInput;

            if (isExitingState) return;

            if (Player.InputHandler.AttackInput && Player.CanAttack())
            {
                StateMachine.ChangeState(Player.AttackState);
            }
            else if (jumpInput && Player.JumpState.CanJump())
            {
                StateMachine.ChangeState(Player.JumpState);
            }
            else if (!Player.CheckIsGround())
            {
                Player.AirState.StartCoyoteTime();
                StateMachine.ChangeState(Player.AirState);
            }
        }
    }
}