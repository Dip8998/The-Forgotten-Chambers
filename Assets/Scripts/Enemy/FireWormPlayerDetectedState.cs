using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class FireWormPlayerDetectedState : EnemyPlayerDetectedState
    {
        private FireWormController fireWormController;

        public FireWormPlayerDetectedState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
            fireWormController = (FireWormController)enemyController;
        }

        public override void OnUpdate()
        {
            if (Time.time >= startTime + enemyData.longRangeActionTime)
            {
                performLongRangeAction = true;
            }

            if (isPlayerInMaxRange && fireWormController.CanShootFireball())
            {
                stateMachine.ChangeState(fireWormController.FireballAttackState);
            }
            else if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }
            else if (performLongRangeAction)
            {
                stateMachine.ChangeState(enemy.ChargeState);
            }
            else if (!isPlayerInMaxRange)
            {
                stateMachine.ChangeState(enemy.LookForPlayerState);
            }
        }
    }
}