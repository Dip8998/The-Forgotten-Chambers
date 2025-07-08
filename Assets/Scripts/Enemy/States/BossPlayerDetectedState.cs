using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Enemy
{
    public class BossPlayerDetectedState : EnemyPlayerDetectedState
    {
        private BossController boss;

        public BossPlayerDetectedState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
            boss = (BossController)enemyController;
        }

        public override void OnUpdate()
        {
            if (Time.time >= startTime + enemyData.longRangeActionTime)
                performLongRangeAction = true;

            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }
            else if (performLongRangeAction && boss.CanShootFireball())
            {
                stateMachine.ChangeState(boss.FireballAttackState);
            }
            else if (!isPlayerInMaxRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}
