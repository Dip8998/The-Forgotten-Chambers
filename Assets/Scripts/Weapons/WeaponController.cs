using ForgottonChambers.Bullets;
using ForgottonChambers.Main;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Sound;
using UnityEngine;

namespace ForgottonChambers.Weapons
{
    public class WeaponController
    {
        private WeaponScriptableObject _weaponScriptableObject;
        private WeaponView _weaponView;

        private BoxCollider2D _hit1Box;
        private CapsuleCollider2D _hit2Box;

        private Animator _animator;
        private PlayerAttackState _state;

        private int _attackCounter;
        private BulletShooter _bulletShooter;

        public WeaponScriptableObject WeaponData => _weaponScriptableObject;
        public bool IsAttacking => _animator != null && _animator.GetBool("attack");
        public WeaponView WeaponView => _weaponView;

        public WeaponController(WeaponScriptableObject weaponScriptableObject, BulletShooter bulletShooter)
        {
            _weaponScriptableObject = weaponScriptableObject;
            _bulletShooter = bulletShooter;
        }

        public void SetWeaponView(WeaponView view)
        {
            _weaponView = view;
            _animator = _weaponView.GetComponent<Animator>();
            _hit1Box = _weaponView.Hit1Box;
            _hit2Box = _weaponView.Hit2Box;

            if (_hit1Box != null)
            {
                _hit1Box.isTrigger = true;
                _hit1Box.enabled = false;
            }

            if (_hit2Box != null)
            {
                _hit2Box.isTrigger = true;
                _hit2Box.enabled = false;
            }
        }

        public void InitializeWeapon(PlayerAttackState state)
        {
            _state = state;
        }

        public void EnterWeapon()
        {
            _weaponView.gameObject.SetActive(true);

            if (_attackCounter >= _weaponScriptableObject.attackMovementSpeeds.Length)
                _attackCounter = 0;

            _animator?.SetBool("attack", true);
            _animator?.SetInteger("attackCounter", _attackCounter);

            switch (_weaponScriptableObject.weaponType)
            {
                case WeaponType.Punch:
                    GameService.Instance.SoundService.Play(Sounds.PLAYERPUNCH);
                    break;
                case WeaponType.Sword:
                    GameService.Instance.SoundService.Play(Sounds.PLAYERSWORDATTACK);
                    break;
                case WeaponType.Gun:
                    GameService.Instance.SoundService.Play(Sounds.PLAYERGUNSHOT);
                    break;
            }
        }

        public void ExitWeapon()
        {
            _animator?.SetBool("attack", false);
            _attackCounter++;
            _weaponView.gameObject.SetActive(false);
        }

        public void AnimationFinishTrigger() => _state?.AnimationFinishTrigger();
        public void AnimationStartMovementTrigger() =>
            _state?.SetPlayerVelocity(_weaponScriptableObject.attackMovementSpeeds.Length > _attackCounter
                ? _weaponScriptableObject.attackMovementSpeeds[_attackCounter]
                : 0);
        public void AnimationStopMovementTrigger() => _state?.SetPlayerVelocity(0);
        public void AnimationTurnOffFlipTrigger() => _state?.SetFlipCheck(false);
        public void AnimationTurnOnFlipTrigger() => _state?.SetFlipCheck(true);
        public void AnimationTurnOnWeapon1HitBoxTrigger() { if (_hit1Box != null) _hit1Box.enabled = true; }
        public void AnimationTurnOffWeapon1HitBoxTrigger() { if (_hit1Box != null) _hit1Box.enabled = false; }
        public void AnimationTurnOnWeapon2HitBoxTrigger() { if (_hit2Box != null) _hit2Box.enabled = true; }
        public void AnimationTurnOffWeapon2HitBoxTrigger() { if (_hit2Box != null) _hit2Box.enabled = false; }
        public void AnimationBulletShootTrigger()
        {
            _bulletShooter.Shoot();
        }
    }
}
