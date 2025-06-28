using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyPlayerDetectedState : EnemyState
    {
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;
        protected bool performLongRangeAction;
        protected bool performCloseRangeAction;

        public EnemyPlayerDetectedState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            performLongRangeAction = false;
            enemy.SetVelocity(0);
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            performCloseRangeAction = enemy.CheckIsPlayerInCloseRange();
        }

        public override void OnUpdate()
        {
            if (Time.time >= startTime + enemyData.longRangeActionTime)
            {
                performLongRangeAction = true;
            }

            if (performCloseRangeAction)
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

        public override void OnFixedUpdate()
        {
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            performCloseRangeAction = enemy.CheckIsPlayerInCloseRange();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}