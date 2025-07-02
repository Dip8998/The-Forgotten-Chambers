using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyKnockbackState : EnemyState
    {
        private float knockbackStartTime;
        private float gravityPauseDuration = 0.15f;
        private float originalGravity;

        public EnemyKnockbackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            knockbackStartTime = Time.time;

            originalGravity = enemy.Enemy.Rigidbody.gravityScale;
            enemy.Enemy.Rigidbody.gravityScale = 0;

            enemy.Enemy.Rigidbody.linearVelocity = Vector2.zero;

            Vector2 hitSource = enemy.LastHitSource; 
            Vector2 knockDirection = ((Vector2)enemy.Enemy.transform.position - hitSource).normalized;

            Vector2 knockbackVelocity = new Vector2(
                knockDirection.x * enemyData.knockBackSpeed,
                enemyData.damageHopSpeed
            );

            enemy.SetVelocity(knockbackVelocity);
        }

        public override void OnUpdate()
        {
            if (Time.time >= knockbackStartTime + gravityPauseDuration)
            {
                enemy.Enemy.Rigidbody.gravityScale = originalGravity;
            }

            if (Time.time >= knockbackStartTime + enemyData.knockBackTime)
            {
                if (enemy.CheckIsPlayerInMinRange() || enemy.CheckIsPlayerInCloseRange())
                {
                    stateMachine.ChangeState(enemy.PlayerDetectedState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.LookForPlayerState);
                }
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            enemy.Enemy.Rigidbody.gravityScale = originalGravity;
            enemy.SetVelocity(0);
        }
    }
}
