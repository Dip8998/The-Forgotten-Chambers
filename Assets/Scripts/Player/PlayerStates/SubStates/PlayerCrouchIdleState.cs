using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Player.SetVelocityZero();
            Player.SetColliderSize(PlayerData.playerCrouchColliderSize, PlayerData.playerCrouchColliderOffset);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Player.SetColliderSize(PlayerData.playerStandColliderSize, PlayerData.playerStandColliderOffset);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            if (moveInput != 0)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
            else if (verticalInput != -1 && !Player.CheckIsCeiling())
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}