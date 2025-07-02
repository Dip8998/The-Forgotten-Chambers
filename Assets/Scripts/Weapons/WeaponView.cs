using ForgottonChambers.Bullets;
using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Weapons
{
    [RequireComponent(typeof(Animator))]
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponScriptableObject _weaponData;
        [SerializeField] private BoxCollider2D _hit1Box;
        [SerializeField] private CapsuleCollider2D _hit2Box;
        [SerializeField] private BulletShooter _bulletShooter;

        public BoxCollider2D Hit1Box => _hit1Box;
        public CapsuleCollider2D Hit2Box => _hit2Box;

        public WeaponController WeaponController { get; private set; }

        protected virtual void Awake()
        {
            WeaponController = new WeaponController(_weaponData, _bulletShooter);
            WeaponController.SetWeaponView(this);
        }

        protected virtual void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Weapon Hit: " + collision.name);

            if (collision.TryGetComponent<EnemyView>(out var enemy))
            {
                enemy.Controller.Damage(10, transform.position);
            }
        }

        public virtual void AnimationFinishTrigger() => WeaponController.AnimationFinishTrigger();
        public virtual void AnimationStartMovementTrigger() => WeaponController.AnimationStartMovementTrigger();
        public virtual void AnimationStopMovementTrigger() => WeaponController.AnimationStopMovementTrigger();
        public virtual void AnimationTurnOffFlipTrigger() => WeaponController.AnimationTurnOffFlipTrigger();
        public virtual void AnimationTurnOnFlipTrigger() => WeaponController.AnimationTurnOnFlipTrigger();
        public void AnimationTurnOnWeapon1HitBoxTrigger() => WeaponController.AnimationTurnOnWeapon1HitBoxTrigger();
        public void AnimationTurnOffWeapon1HitBoxTrigger() => WeaponController.AnimationTurnOffWeapon1HitBoxTrigger();
        public void AnimationTurnOnWeapon2HitBoxTrigger() => WeaponController.AnimationTurnOnWeapon2HitBoxTrigger();
        public void AnimationTurnOffWeapon2HitBoxTrigger() => WeaponController.AnimationTurnOffWeapon2HitBoxTrigger();
    }
}
