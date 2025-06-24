using ForgottonChambers.Weapons;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerAttackState : PlayerAbilityState
    {
        public WeaponController CurrentWeapon { get; private set; }
        private float _velocityToSet;
        private bool _setVelocity;
        private float _xInput;
        private bool _shouldCheckFlip;

        public PlayerAttackState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _setVelocity = false;
            if (CurrentWeapon != null)
            {
                CurrentWeapon.EnterWeapon();
            }
            else
            {
                isAbilityDone = true;
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            if (CurrentWeapon != null)
            {
                CurrentWeapon.ExitWeapon();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            _xInput = Player.InputHandler.MoveInput;

            if (_shouldCheckFlip)
            {
                Player.CheckIfShouldFlip(_xInput);
            }

            if (_setVelocity)
            {
                Player.SetVelocityX(_velocityToSet * Player.FacingDirection);
            }
        }

        public void SetWeapon(WeaponView weaponView)
        {
            if (weaponView == null) return;

            CurrentWeapon = weaponView.WeaponController;
            CurrentWeapon.InitializeWeapon(this);
        }


        public void SetPlayerVelocity(float velocity)
        {
            _velocityToSet = velocity;
            _setVelocity = true;
        }

        public void SetFlipCheck(bool value)
        {
            _shouldCheckFlip = value;
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
            isAbilityDone = true;
        }
    }
}