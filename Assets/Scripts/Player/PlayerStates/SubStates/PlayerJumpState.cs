using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int amountOfJumpsLeft;

        public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
            amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            if (player.playerView.HasBoxAttached())
            {
                isAbilityDone = true; 
                return;
            }
            player.SetVelocityY(playerData.playerJumpForce);
            isAbilityDone = true;
            amountOfJumpsLeft--;
        }

        public bool CanJump()
        {
            if(amountOfJumpsLeft > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetAmountJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--; 
    }
}
