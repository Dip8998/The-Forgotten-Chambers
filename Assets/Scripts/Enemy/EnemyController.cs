using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using ForgottonChambers.Enemy; // Ensure this is present for EnemyType

namespace ForgottonChambers.Enemy
{
    public class EnemyController
    {
        public EnemyView Enemy { get; private set; }
        protected EnemyScriptableObject enemyData;
        protected EnemyStateMachine stateMachine;
        protected Transform playerTransform;
        private int currentHealth;

        public EnemyIdleState IdleState { get; protected set; }
        public EnemyMoveState MoveState { get; protected set; }
        public EnemyPlayerDetectedState PlayerDetectedState { get; protected set; }
        public EnemyChargeState ChargeState { get; protected set; }
        public EnemyLookForPlayerState LookForPlayerState { get; protected set; }
        public EnemyMeleeAttackState MeleeAttackState { get; protected set; }
        public EnemyKnockbackState KnockbackState { get; protected set; }

        protected Vector2 velocityWorkSpace;

        public int FacingDirection { get; private set; }
        public int LastDamageDirection { get; private set; }

        public EnemyController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
        {
            FacingDirection = 1;
            this.Enemy = enemyView;
            this.enemyData = enemyData;
            this.playerTransform = playerTransform;
            currentHealth = enemyData.enemyHealth;
            stateMachine = new EnemyStateMachine();

            Debug.Log($"EnemyController initialized for {enemyData.enemyType} enemy."); // Keep this for now for debug

            InitializeStates();
            stateMachine.Initialize(MoveState);
        }

        protected virtual void InitializeStates()
        {
            IdleState = new EnemyIdleState(stateMachine, this, enemyData, EnemyState.ANIM_IDLE);
            MoveState = new EnemyMoveState(stateMachine, this, enemyData, EnemyState.ANIM_MOVE);
            PlayerDetectedState = new EnemyPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);
            ChargeState = new EnemyChargeState(stateMachine, this, enemyData, EnemyState.ANIM_CHARGE);
            LookForPlayerState = new EnemyLookForPlayerState(stateMachine, this, enemyData, EnemyState.ANIM_LOOK_FOR_PLAYER);
            MeleeAttackState = new EnemyMeleeAttackState(stateMachine, this, enemyData, EnemyState.ANIM_MELEE_ATTACK);
            KnockbackState = new EnemyKnockbackState(stateMachine, this, enemyData, 0);
        }

        public void UpdateController()
        {
            stateMachine.CurrentState.OnUpdate();
        }

        public void FixedUpdateController()
        {
            stateMachine.CurrentState.OnFixedUpdate();
        }

        public void SetVelocity(float speed)
        {
            velocityWorkSpace.Set(FacingDirection * speed, Enemy.Rigidbody.linearVelocity.y);
            Enemy.Rigidbody.linearVelocity = velocityWorkSpace;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Enemy.Rigidbody.linearVelocity = velocity;
        }

        public void Damage(int damage)
        {
            currentHealth -= damage;

            GameService.Instance.ParticleService.PlayParticle(ParticleType.EnemyHit, Enemy.transform.position, Quaternion.identity);

            SetDamageDirection();

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                stateMachine.ChangeState(KnockbackState);
            }
        }

        private void SetDamageDirection()
        {
            if (playerTransform != null)
            {
                if (Enemy.transform.position.x < playerTransform.position.x)
                    LastDamageDirection = -1;
                else
                    LastDamageDirection = 1;
            }
            else
            {
                LastDamageDirection = FacingDirection;
            }
        }

        public void Die()
        {
            ParticleType deathParticleType = ParticleType.EnemyDeath; // Default particle

            switch (enemyData.enemyType)
            {
                case EnemyType.CrawlerCrab:
                    deathParticleType = ParticleType.EnemyCrabDeath;
                    break;
                case EnemyType.FireSplitterWorm:
                    deathParticleType = ParticleType.EnemyFireWormDeath;
                    break;
                case EnemyType.GroundedSkeleton:
                    deathParticleType = ParticleType.EnemySkeletonDeath;
                    break;
                default:
                    Debug.LogWarning($"No specific death particle defined for enemy type: {enemyData.enemyType}. Using default EnemyDeath particle.");
                    break;
            }

            GameService.Instance.ParticleService.PlayParticle(deathParticleType, Enemy.transform.position, Quaternion.identity);
            Enemy.DestroyGameObject();
        }

        public void DamageHop(float velocityX, float velocityY)
        {
            velocityWorkSpace.Set(velocityX, velocityY);
            Enemy.Rigidbody.linearVelocity = velocityWorkSpace;
        }

        public Transform PlayerPosition()
        {
            return playerTransform;
        }

        public bool CheckIsHittingWall() => AllChecks(Enemy.CastPosition, enemyData.castDistance, 0, Color.blue, enemyData.groundLayer);

        public bool CheckIsNearEdge() => !AllChecks(Enemy.CastPosition, 0, -enemyData.castDistance, Color.red, enemyData.groundLayer);

        public bool CheckIsPlayerInMinRange() => AllChecks(Enemy.PlayerCheck, enemyData.minPlayerDetectedDistance, 0, Color.green, enemyData.playerLayer);

        public bool CheckIsPlayerInMaxRange() => AllChecks(Enemy.PlayerCheck, enemyData.maxPlayerDetectedDistance, 0, Color.green, enemyData.playerLayer);

        public bool CheckIsPlayerInCloseRange() => AllChecks(Enemy.PlayerCheck, enemyData.closeRangeActionDistance, 0, Color.yellow, enemyData.playerLayer);

        protected bool AllChecks(Transform transform, float DistanceX, float DistanceY, Color color, LayerMask layer)
        {
            Vector3 target = transform.position + new Vector3(DistanceX * FacingDirection, DistanceY, 0);
            Debug.DrawLine(transform.position, target, color);
            return Physics2D.Linecast(transform.position, target, layer);
        }

        public void Flip()
        {
            FacingDirection *= -1;
            Enemy.FlipDirection(FacingDirection == 1);
        }

        public void AnimationAttackTrigger()
        {
            if (stateMachine.CurrentState is EnemyAttackState attackState)
            {
                attackState.AnimationAttackTrigger();
            }
        }

        public void AnimationFinishedTrigger()
        {
            if (stateMachine.CurrentState is EnemyAttackState attackState)
            {
                attackState.AnimationFinishedTrigger();
            }
        }
    }
}