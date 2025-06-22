using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int _amountOfJumpsLeft;
        public int AmountOfJumpsLeft => _amountOfJumpsLeft;

        public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
            _amountOfJumpsLeft = PlayerData.amountOfJumps;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            if (Player.PlayerView.HasBoxAttached())
            {
                isAbilityDone = true;
                return;
            }

            float jumpForce = _amountOfJumpsLeft == PlayerData.amountOfJumps ? PlayerData.playerJumpForce : PlayerData.playerDoubleJumpForce;
            Player.SetVelocityY(jumpForce);

            isAbilityDone = true;
            _amountOfJumpsLeft--;
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }

        public void ResetAmountJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}