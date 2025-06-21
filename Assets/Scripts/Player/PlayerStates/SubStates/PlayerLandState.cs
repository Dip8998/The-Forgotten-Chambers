using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            if(!isExitingState)
            {
                if (moveInput != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
                else if (isAnimationFinished)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
    }
}
