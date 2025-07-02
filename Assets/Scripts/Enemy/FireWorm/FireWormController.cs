using ForgottonChambers.Bullets;
using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class FireWormController : EnemyController
    {
        public  EnemyFireballAttackState FireballAttackState { get; private set; }
        private float lastFireballTime;

        public FireWormController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {
            lastFireballTime = -enemyData.fireballCooldown;
        }

        protected override void InitializeStates()
        {
            base.InitializeStates();

            PlayerDetectedState = new FireWormPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);

            FireballAttackState = new EnemyFireballAttackState(stateMachine, this, enemyData, EnemyState.ANIM_FIREBALL_ATTACK);
        }

        public bool CanShootFireball()
        {
            bool cooldownReady = Time.time >= lastFireballTime + enemyData.fireballCooldown;
            bool notInKnockback = stateMachine.CurrentState != KnockbackState; 
            return cooldownReady && notInKnockback;
        }

        public void ShootFireball()
        {
            if (CanShootFireball())
            {
                lastFireballTime = Time.time;
                GameObject fireballGO = GameObject.Instantiate(enemyData.fireballPrefab, Enemy.AttackPosition.position, Quaternion.identity);
                FireballProjectile fireball = fireballGO.GetComponent<FireballProjectile>();
                if (fireball != null)
                {
                    fireball.Initialize(FacingDirection, enemyData.fireballSpeed, enemyData.fireballDamage, fireball.lifetime);
                }
            }
        }
    }
}