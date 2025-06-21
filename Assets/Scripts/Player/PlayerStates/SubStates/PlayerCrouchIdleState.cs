using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            player.SetVelocityZero();
            player.SetColliderSize(
                   new Vector2(1.0f, 0.5f),    
                   new Vector2(0.0f, -0.25f));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            player.SetColliderSize(
                new Vector2(1.0f, 1.8f),    
                new Vector2(0.0f, 0.0f));
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!isExitingState)
            {
                if(moveInput != 0)
                {
                    stateMachine.ChangeState(player.CrouchMoveState);
                }
                else if(crouchInput != -1 && !player.CheckIsCeiling())
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
    }
}