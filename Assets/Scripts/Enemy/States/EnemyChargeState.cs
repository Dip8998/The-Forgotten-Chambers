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

        public EnemyChargeState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            isHittingWall = enemy.CheckIsHittingWall();
            isNearLedge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();

            if (isHittingWall || isNearLedge)
            {
                enemy.SetVelocity(0);
                stateMachine.ChangeState(enemy.LookForPlayerState);
                return;
            }
            enemy.SetVelocity(enemyData.chargeSpeed);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            isHittingWall = enemy.CheckIsHittingWall();
            isNearLedge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();

            isChargeTimeOver = false;
            enemy.SetVelocity(enemyData.chargeSpeed);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(Time.time >= startTime + enemyData.chargeTime)
            {
                isChargeTimeOver = true;
            }

            if(isChargeTimeOver)
            {
                if (isPlayerInMinRange)
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