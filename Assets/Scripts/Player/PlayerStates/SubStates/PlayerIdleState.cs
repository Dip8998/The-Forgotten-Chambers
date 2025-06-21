using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            player.SetVelocityX(0);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!isExitingState)
            {
                if (moveInput != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
                else if (crouchInput == -1)
                {
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
            }  
        }
    }
}
