using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class FireWormController : EnemyController
    {
        public EnemyFireballAttackState FireballAttackState { get; private set; }
        private float lastFireballTime;

        public FireWormController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {
            lastFireballTime = -enemyData.fireballCooldown;
        }

        protected override void InitializeStates()
        {
            IdleState = new EnemyIdleState(stateMachine, this, enemyData, EnemyState.ANIM_IDLE);
            MoveState = new EnemyMoveState(stateMachine, this, enemyData, EnemyState.ANIM_MOVE);
            PlayerDetectedState = new FireWormPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);
            ChargeState = new EnemyChargeState(stateMachine, this, enemyData, EnemyState.ANIM_CHARGE);
            LookForPlayerState = new EnemyLookForPlayerState(stateMachine, this, enemyData, EnemyState.ANIM_LOOK_FOR_PLAYER);
            MeleeAttackState = new EnemyMeleeAttackState(stateMachine, this, enemyData, EnemyState.ANIM_MELEE_ATTACK);
            KnockbackState = new EnemyKnockbackState(stateMachine, this, enemyData, 0);

            FireballAttackState = new EnemyFireballAttackState(stateMachine, this, enemyData, EnemyState.ANIM_FIREBALL_ATTACK);
        }

        public bool CanShootFireball()
        {
            bool cooldownReady = Time.time >= lastFireballTime + enemyData.fireballCooldown;

            bool notInKnockback = stateMachine != null && stateMachine.CurrentState != KnockbackState;

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

        public bool CheckIsPlayerInRangedAttackRange() => AllChecks(Enemy.PlayerCheck, enemyData.rangedAttackDistance, 0, Color.magenta, enemyData.playerLayer);
    }
}