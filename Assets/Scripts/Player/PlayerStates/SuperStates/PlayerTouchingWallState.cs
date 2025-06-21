using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerTouchingWallState : PlayerState
    {
        protected float xInput;
        protected float yInput;
        protected bool jumpInput;
        protected bool grabInput;

        public PlayerTouchingWallState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
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
            xInput = player.InputHandler.MoveInput;
            yInput = player.InputHandler.UpInput;
            grabInput = player.InputHandler.GrabInput;
            jumpInput = player.InputHandler.JumpInput;

            if (jumpInput)
            {
                player.WallJumpState.DetermineWallJumpDirection(player.CheckIsWall());
                stateMachine.ChangeState(player.WallJumpState); 
            }
            else if (player.CheckIsGround() && !grabInput)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if(!player.CheckIsWall() || (xInput != player.FacingDirection && !grabInput))
            {
                stateMachine.ChangeState(player.AirState);
            }
        }
    }
}