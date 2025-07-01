using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyKnockbackState : EnemyState
    {
        private float knockbackStartTime;

        public EnemyKnockbackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            knockbackStartTime = Time.time;

            enemy.Enemy.Rigidbody.linearVelocity = Vector2.zero;

            Vector2 knockbackVelocity = new Vector2(
                enemyData.knockBackSpeed * -enemy.FacingDirection,
                enemyData.damageHopSpeed
            );

            enemy.SetVelocity(knockbackVelocity);
        }

        public override void OnUpdate()
        {
            if (Time.time >= knockbackStartTime + enemyData.knockBackTime)
            {
                stateMachine.ChangeState(enemy.MoveState);
            }
        }
    }
}
