using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyMeleeAttackState : EnemyAttackState
    {
        public EnemyMeleeAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnUpdate()
        {
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

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void AnimationAttackTrigger()
        {
            base.AnimationAttackTrigger();

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(enemy.Enemy.AttackPosition.position, enemyData.attackRadius, enemyData.playerLayer);

            foreach (Collider2D obj in detectedObjects)
            {
                enemy.Attack(enemyData.attackDamage);
            }
        }

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}