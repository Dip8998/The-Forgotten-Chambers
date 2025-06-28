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

        public EnemyPlayerDetectedState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            performCloseRangeAction = enemy.CheckIsPlayerInCloseRange();
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

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(Time.time >= startTime + enemyData.longRangeActionTime)
            {
                performLongRangeAction = true;
            }

            if(performCloseRangeAction)
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
