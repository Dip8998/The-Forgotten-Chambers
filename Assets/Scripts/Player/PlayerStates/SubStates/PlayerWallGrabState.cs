using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        private Vector2 _holdPosition;

        public PlayerWallGrabState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _holdPosition = Player.PlayerView.transform.position;
            HoldPosition();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            HoldPosition();

            if (yInput > 0)
            {
                StateMachine.ChangeState(Player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
        }

        private void HoldPosition()
        {
            Player.PlayerView.transform.position = _holdPosition;
            Player.SetVelocityZero();
        }
    }
}