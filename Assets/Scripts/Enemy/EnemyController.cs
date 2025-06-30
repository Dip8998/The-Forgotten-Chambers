using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace ForgottonChambers.Enemy
{
    public class EnemyController
    {
        public EnemyView Enemy { get; private set; }
        private EnemyScriptableObject enemyData;
        private EnemyStateMachine stateMachine;
        private Transform playerTransform;
        private int currentHealth;

        public EnemyIdleState IdleState { get; private set; }
        public EnemyMoveState MoveState { get; private set; }
        public EnemyPlayerDetectedState PlayerDetectedState { get; private set; }
        public EnemyChargeState ChargeState { get; private set; }
        public EnemyLookForPlayerState LookForPlayerState { get; private set; }
        public EnemyMeleeAttackState MeleeAttackState { get; private set; }

        private Vector2 velocityWorkSpace;

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
            InitializeStates();
            stateMachine.Initialize(MoveState);
        }

        private void InitializeStates()
        {
            IdleState = new EnemyIdleState(stateMachine, this, enemyData, EnemyState.ANIM_IDLE);
            MoveState = new EnemyMoveState(stateMachine, this, enemyData, EnemyState.ANIM_MOVE);
            PlayerDetectedState = new EnemyPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);
            ChargeState = new EnemyChargeState(stateMachine, this, enemyData, EnemyState.ANIM_CHARGE);
            LookForPlayerState = new EnemyLookForPlayerState(stateMachine, this, enemyData, EnemyState.ANIM_LOOK_FOR_PLAYER);
            MeleeAttackState = new EnemyMeleeAttackState(stateMachine, this, enemyData, EnemyState.ANIM_MELEE_ATTACK);
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

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
            Enemy.Rigidbody.linearVelocity = velocityWorkSpace;
        }

        public void Attack(int damage)
        {
            GameService.Instance.PlayerService.GetPlayerController().Damage(damage);
        }

        public void Damage(int damage)
        {
            currentHealth -= damage;
            if(currentHealth <= 0)
            {
                Die();
            }
            SetDamageDirection();
        }

        public void Die()
        {
            GameService.Instance.ParticleService.PlayParticle(ParticleType.EnemyDeath, Enemy.transform.position, Quaternion.identity);
            Enemy.DestroyGameObject();
        }

        private void SetDamageDirection()
        {
            GameService.Instance.ParticleService.PlayParticle(ParticleType.EnemyHit, Enemy.transform.position, Quaternion.identity);

            DamageHop(enemyData.damageHopSpeed);

            if (playerTransform != null)
            {
                if (Enemy.transform.position.x < playerTransform.position.x)
                    LastDamageDirection = -1;
                else
                    LastDamageDirection = 1;
            }
            else
            {
                Debug.LogError("Player Transform is not assigned to EnemyView or EnemyController.", Enemy);
                LastDamageDirection = FacingDirection;
            }

            SetVelocity(enemyData.knockBackSpeed, -enemyData.knockBackAngle * FacingDirection, LastDamageDirection);
        }

        public void DamageHop(float velocity)
        {
            velocityWorkSpace.Set(Enemy.Rigidbody.linearVelocity.x, velocity);
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

        private bool AllChecks(Transform transform, float DistanceX, float DistanceY, Color color, LayerMask layer)
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