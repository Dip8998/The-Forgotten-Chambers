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

        public EnemyIdleState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash) { }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetVelocity(0);
            isIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void OnUpdate()
        {
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

        public override void OnFixedUpdate()
        {
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
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
    }
}