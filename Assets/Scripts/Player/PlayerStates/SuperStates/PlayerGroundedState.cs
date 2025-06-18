using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerGroundedState : PlayerState
    {
        protected float input;

        public PlayerGroundedState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
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
            input = player.InputHandler.MoveInput;
        }
    }
}
