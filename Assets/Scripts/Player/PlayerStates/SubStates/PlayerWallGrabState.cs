using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        private Vector2 holdPosition;

        public PlayerWallGrabState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
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
            holdPosition = player.playerView.transform.position;
            HoldPosition();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            HoldPosition();

            if (yInput > 0)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if(yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }

        private void HoldPosition()
        {
            player.playerView.transform.position = holdPosition;

            player.SetVelocityX(0);
            player.SetVelocityY(0);
        }
    }
}
