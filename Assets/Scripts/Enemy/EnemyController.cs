using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.UI;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyController
    {
        public EnemyView Enemy { get; private set; }
        public EnemyScriptableObject enemyData{ get; private set; }
        protected EnemyStateMachine stateMachine;
        protected Transform playerTransform;
        public int CurrentHealth;

        public EnemyIdleState IdleState { get; protected set; }
        public EnemyMoveState MoveState { get; protected set; }
        public EnemyPlayerDetectedState PlayerDetectedState { get; protected set; }
        public EnemyChargeState ChargeState { get; protected set; }
        public EnemyLookForPlayerState LookForPlayerState { get; protected set; }
        public EnemyMeleeAttackState MeleeAttackState { get; protected set; }
        public EnemyKnockbackState KnockbackState { get; protected set; }

        protected Vector2 velocityWorkSpace;

        public Vector2 LastHitSource { get; set; }
        public int FacingDirection { get; private set; }
        public int LastDamageDirection { get; private set; }
        private bool hasFacedPlayerThisState;
        public UIService UIService => GameService.Instance.UIService;

        public EnemyController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
        {
            FacingDirection = 1;
            this.Enemy = enemyView;
            this.enemyData = enemyData;
            this.playerTransform = playerTransform;
            CurrentHealth = enemyData.enemyHealth;
            stateMachine = new EnemyStateMachine();

            Debug.Log($"EnemyController initialized for {enemyData.enemyType} enemy.");

            InitializeStates();
            stateMachine.Initialize(MoveState);
            Enemy.SetHealthBar(CurrentHealth, enemyData.enemyHealth);
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

        public virtual void Damage(int damage, Vector2 hitSourcePosition)
        {
            LastHitSource = hitSourcePosition;
            CurrentHealth -= damage;
            Enemy.SetHealthBar(CurrentHealth, enemyData.enemyHealth);
            GameService.Instance.ParticleService.PlayParticle(ParticleType.EnemyHit, Enemy.transform.position, Quaternion.identity);

            SetDamageDirection();

            if (CurrentHealth <= 0)
            {
                Die();
                UIService.AddScore(enemyData.deathScore);
            }
            else
            {
                stateMachine.ChangeState(KnockbackState);
            }
        }

        protected void SetDamageDirection()
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
            ParticleType deathParticleType = ParticleType.EnemyDeath; 

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
                case EnemyType.Boss:
                    deathParticleType = ParticleType.BossDeath;
                    break;
                default:
                    Debug.LogWarning($"No specific death particle defined for enemy type: {enemyData.enemyType}. Using default EnemyDeath particle.");
                    break;
            }

            GameService.Instance.ParticleService.PlayParticle(deathParticleType, Enemy.ParticleTransform.transform.position, Quaternion.identity);

            SkeletonView skeletonView = Enemy as SkeletonView;
            CrabView crabView = Enemy as CrabView;

            if (skeletonView != null && skeletonView.ItemDrop != null)
            {
                Object.Instantiate(skeletonView.ItemDrop, skeletonView.ItemDropPos.position, Quaternion.identity);
            }
            else if(crabView != null && crabView.ItemDrop != null)
            {
                Object.Instantiate(crabView.ItemDrop, crabView.ItemDropPos.position, Quaternion.identity);
            }
            if (crabView != null && crabView.SwitchActive != null)
            {
                crabView.SwitchActive.gameObject.SetActive(true);
            }

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

        public bool CheckIsPlayerInMinRange()
        {
            return OverlapPlayerRange(enemyData.minPlayerDetectedDistance, Color.green);
        }

        public bool CheckIsPlayerInCloseRange()
        {
            return OverlapPlayerRange(enemyData.closeRangeActionDistance, Color.yellow);
        }

        public bool CheckIsPlayerInMaxRange() =>
            CheckBothSides(Enemy.PlayerCheck, enemyData.maxPlayerDetectedDistance, 0, Color.cyan, enemyData.playerLayer);


        private bool CheckBothSides(Transform origin, float distanceX, float distanceY, Color color, LayerMask layer)
        {
            Vector3 rightTarget = origin.position + new Vector3(distanceX, distanceY, 0);
            Vector3 leftTarget = origin.position - new Vector3(distanceX, -distanceY, 0);

            Debug.DrawLine(origin.position, rightTarget, color);
            Debug.DrawLine(origin.position, leftTarget, color);

            bool hitRight = Physics2D.Linecast(origin.position, rightTarget, layer);
            bool hitLeft = Physics2D.Linecast(origin.position, leftTarget, layer);

            return hitRight || hitLeft;
        }

        public void FacePlayerIfNeeded()
        {
            if (hasFacedPlayerThisState) return;

            Transform player = GameService.Instance.PlayerService.GetPlayerController().PlayerView.transform;
            if (player.position.x < Enemy.transform.position.x && FacingDirection == 1)
                Flip();
            else if (player.position.x > Enemy.transform.position.x && FacingDirection == -1)
                Flip();

            hasFacedPlayerThisState = true;
        }

        public void ResetFacingFlag() => hasFacedPlayerThisState = false;

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

        private bool OverlapPlayerRange(float radius, Color debugColor)
        {
            Vector2 origin = Enemy.PlayerCheck.position;

            DebugDrawCircle(origin, radius, debugColor);

            Collider2D hit = Physics2D.OverlapCircle(origin, radius, enemyData.playerLayer);
            return hit != null;
        }

        private void DebugDrawCircle(Vector2 center, float radius, Color color, int segments = 32)
        {
            float angle = 0f;
            Vector3 prevPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            for (int i = 1; i <= segments; i++)
            {
                angle = 2 * Mathf.PI * i / segments;
                Vector3 newPoint = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                Debug.DrawLine(prevPoint, newPoint, color);
                prevPoint = newPoint;
            }
        }

    }
}