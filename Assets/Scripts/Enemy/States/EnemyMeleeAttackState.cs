using ForgottonChambers.HealthSystem;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyMeleeAttackState : EnemyAttackState
    {
        public EnemyMeleeAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName)
        {
        }

        public override void AnimationAttackTrigger()
        {
            base.AnimationAttackTrigger();

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(enemy.EnemyView.AttackPosition.position, enemyData.attackRadius, enemyData.playerLayer);

            foreach (Collider2D obj in detectedObjects)
            {
                IHealth health = obj.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(enemyData.attackDamage);
                }
            }
        }

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isAnimationFinished)
            {
                if (isPlayerInMinRange || isPlayerInMaxRange)
                {
                    stateMachine.ChangeState(enemy.PlayerDetectedState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.LookForPlayerState);
                }
            }
        }
    }
}