using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.SetVelocityZero();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            if (moveInput != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (isAnimationFinished)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}