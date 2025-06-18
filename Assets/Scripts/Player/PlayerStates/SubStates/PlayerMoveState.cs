using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
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

            player.SetVelocity(playerData.playerMovementSpeed * input);

            if (input == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }
}
