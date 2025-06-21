using ForgottonChambers.Weapons;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon weapon;
        private float velocityToSet;
        private bool setVelocity;
        private float xInput;
        private bool shouldCheckFlip;

        public PlayerAttackState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName) : base(player, stateMachine, playerDate, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
            isAbilityDone = true;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            setVelocity = false;
            weapon.EnterWeapon();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            weapon.ExitWeapon();
        }

        public void SetWeapon(Weapon weapon)
        {
            this.weapon = weapon;
            weapon.InitializeWeapon(this);
        }

        public void SetPlayerVelocity(float velocity)
        {
            player.SetVelocityX(velocity * player.FacingDirection);
            velocityToSet = velocity;
            setVelocity = true;
        }

        public void SetFlipCheck(bool value)
        {
            shouldCheckFlip = value;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            xInput = player.InputHandler.MoveInput;

            if(shouldCheckFlip)
            {
                player.CheckIfShouldFlip(xInput);
            }

            if (setVelocity)
            {
                player.SetVelocityX(velocityToSet*player.FacingDirection);
            }
        }
    }
}
