using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public abstract class PlayerAbilityState : PlayerState
    {
        protected bool isAbilityDone;

        public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            isAbilityDone = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isAbilityDone && !isExitingState)
            {
                if (Player.CheckIsGround() && Mathf.Abs(Player.CurrentVelocity.y) < 0.01f)
                {
                    StateMachine.ChangeState(Player.IdleState);
                }
                else
                {
                    StateMachine.ChangeState(Player.AirState);
                }
            }
        }
    }
}