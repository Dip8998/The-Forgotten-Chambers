using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyPlayerDetectedState : EnemyState
    {
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;
        protected bool performLongRangeAction;

        public EnemyPlayerDetectedState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            performLongRangeAction = false;
            enemy.SetVelocity(0);
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
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

            if (performLongRangeAction)
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
