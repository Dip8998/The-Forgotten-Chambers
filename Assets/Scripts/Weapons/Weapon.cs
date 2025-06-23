using ForgottonChambers.HealthSystem;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ForgottonChambers.Weapons
{
    [RequireComponent(typeof(Animator))]
    public class Weapon : MonoBehaviour
    {   
        [SerializeField] private WeaponScriptableObject _weaponData;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private BoxCollider2D _hit1Box;
        [SerializeField] private CapsuleCollider2D _hit2Box;

        public WeaponScriptableObject WeaponData => _weaponData;

        protected Animator _animator;
        protected PlayerAttackState _state;

        protected int _attackCounter;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
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

        protected virtual void Start()
        {
            gameObject.SetActive(false);
        }

        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);

            if (_attackCounter >= _weaponData.attackMovementSpeeds.Length)
            {
                _attackCounter = 0;
            }

            _animator?.SetBool("attack", true);
            _animator?.SetInteger("attackCounter", _attackCounter);
        }

        public virtual void ExitWeapon()
        {
            _animator?.SetBool("attack", false);
            _attackCounter++;
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IHealth health))
            {
                health?.TakeDamage(attackDamage);
            }
        }

        public virtual void AnimationFinishTrigger()
        {
            _state?.AnimationFinishTrigger();
        }

        public virtual void AnimationStartMovementTrigger()
        {
            if (_weaponData != null && _attackCounter < _weaponData.attackMovementSpeeds.Length)
            {
                _state?.SetPlayerVelocity(_weaponData.attackMovementSpeeds[_attackCounter]);
            }
            else
            {
                _state?.SetPlayerVelocity(0);
            }
        }

        public virtual void AnimationStopMovementTrigger()
        {
            _state?.SetPlayerVelocity(0);
        }

        public virtual void AnimationTurnOffFlipTrigger()
        {
            _state?.SetFlipCheck(false);
        }

        public virtual void AnimationTurnOnFlipTrigger()
        {
            _state?.SetFlipCheck(true);
        }

        public void AnimationTurnOnWeapon1HitBoxTrigger()
        {
            if (_hit1Box != null)
                _hit1Box.enabled = true;
        }

        public void AnimationTurnOffWeapon1HitBoxTrigger()
        {
            if (_hit1Box != null)
                _hit1Box.enabled = false;
        }

        public void AnimationTurnOnWeapon2HitBoxTrigger()
        {
            if (_hit2Box != null)
                _hit2Box.enabled = true;
        }

        public void AnimationTurnOffWeapon2HitBoxTrigger()
        {
            if (_hit2Box != null)
                _hit2Box.enabled = false;
        }

        public void InitializeWeapon(PlayerAttackState state)
        {
            _state = state;
        }
    }

    public static class WeaponUtilities
    {
        public static Weapon GetWeaponByType(this List<Weapon> weapons, WeaponType type)
        {
            return weapons.FirstOrDefault(w => w.WeaponData.weaponType == type);
        }
    }
}