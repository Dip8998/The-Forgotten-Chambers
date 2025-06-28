using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyMoveState : EnemyState
    {
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;
        protected bool isHittingWall;
        protected bool isNearEdge;

        public EnemyMoveState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName)
            : base(stateMachine, enemyController, enemyData, animBoolName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            isHittingWall = enemy.CheckIsHittingWall();
            isNearEdge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetVelocity(enemyData.movementSpeed);

            isHittingWall = enemy.CheckIsHittingWall();
            isNearEdge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isPlayerInMinRange || isPlayerInMaxRange)
            {
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (isHittingWall || isNearEdge)
            {
                enemy.IdleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}