using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyChargeState : EnemyState
    {
        protected bool isChargeTimeOver;
        protected bool isPlayerInMinRange;
        protected bool isHittingWall;
        protected bool isNearLedge;
        protected bool performCloseRangeAction;

        public EnemyChargeState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            isChargeTimeOver = false;

            isHittingWall = enemy.CheckIsHittingWall();
            isNearLedge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            performCloseRangeAction = enemy.CheckIsPlayerInCloseRange();

            enemy.SetVelocity(enemyData.chargeSpeed);
        }

        public override void OnUpdate()
        {
            if (Time.time >= startTime + enemyData.chargeTime)
            {
                isChargeTimeOver = true;
            }

            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.MeleeAttackState);
            }

            if (isChargeTimeOver)
            {
                if (enemy.CheckIsPlayerInMinRange())
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
            isHittingWall = enemy.CheckIsHittingWall();
            isNearLedge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            performCloseRangeAction = enemy.CheckIsPlayerInCloseRange();

            if (isHittingWall || isNearLedge)
            {
                enemy.SetVelocity(0);
                stateMachine.ChangeState(enemy.LookForPlayerState);
                return;
            }

            if (isPlayerInMinRange)
            {
                enemy.SetVelocity(0);
                stateMachine.ChangeState(enemy.PlayerDetectedState);
                return;
            }

            enemy.SetVelocity(enemyData.chargeSpeed);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            enemy.SetVelocity(0);
        }
    }
}