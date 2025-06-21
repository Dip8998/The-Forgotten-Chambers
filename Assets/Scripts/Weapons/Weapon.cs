using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponScriptableObject weaponData;
        protected Animator animator;
        protected PlayerAttackState state;

        protected int attackCounter;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);

            if(attackCounter >= weaponData.movementSpeed.Length)
            {
                attackCounter = 0;
            }
            animator.SetBool("attack", true);
            animator.SetInteger("attackCounter", attackCounter);
        }

        public virtual void ExitWeapon()
        {
            animator.SetBool("attack", false);

            attackCounter++;
            gameObject.SetActive(false);
        }

        #region Animation triggers
        public virtual void AnimationFinishTrigger()
        {
            state.AnimationFinishTrigger();
        }

        public virtual void AnimationStartMovementTrigger()
        {
            state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
        }

        public virtual void AnimationStopMovementTrigger()
        {
            state.SetPlayerVelocity(0);
        }

        public virtual void AnimationTurnOffFlipTrigger()
        {
            state.SetFlipCheck(false);
        }

        public virtual void AnimationTurnOnFlipTrigger()
        {
            state.SetFlipCheck(true);
        }
        #endregion

        public void InitializeWeapon(PlayerAttackState state)
        {
            this.state = state;
        }
    }
}
