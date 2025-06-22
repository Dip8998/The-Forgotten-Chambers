using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Weapons
{
    [RequireComponent(typeof(Animator))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponScriptableObject _weaponData;
        protected Animator _animator;
        protected PlayerAttackState _state;

        protected int _attackCounter;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
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

        #region Animation Triggers

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
        #endregion

        public void InitializeWeapon(PlayerAttackState state)
        {
            _state = state;
        }
    }
}