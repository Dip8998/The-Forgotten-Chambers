using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyIdleState : EnemyState
    {
        protected bool flipAfterIdle;
        protected float idleTime;
        protected bool isIdleTimeOver;
        protected bool isPlayerInMinRange;

        public EnemyIdleState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetVelocity(0);
            isIdleTimeOver = false;
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            SetRandomIdleTime();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isPlayerInMinRange)
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (Time.time >= startTime + idleTime)
            {
                isIdleTimeOver = true;
                stateMachine.ChangeState(enemy.MoveState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            if (flipAfterIdle && isIdleTimeOver)
            {
                enemy.Flip();
            }

            flipAfterIdle = false;
        }

        public void SetFlipAfterIdle(bool flip)
        {
            this.flipAfterIdle = flip;
        }

        private void SetRandomIdleTime()
        {
            idleTime = Random.Range(enemyData.minIdleTime, enemyData.maxIdleTime);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
        }
    }
}
