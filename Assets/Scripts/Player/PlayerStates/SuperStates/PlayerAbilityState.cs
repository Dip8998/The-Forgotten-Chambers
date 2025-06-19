using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool isAbilityDone;

        public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            isAbilityDone = false;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (isAbilityDone)
            {
                if(player.CheckIsGround() && player.CurrentVelocity.y < 0.01f)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.AirState);
                }
            }
        }
    }
}
