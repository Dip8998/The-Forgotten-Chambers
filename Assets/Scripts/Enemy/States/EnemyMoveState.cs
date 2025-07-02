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

        public EnemyMoveState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
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
            if (isPlayerInMinRange || isPlayerInMaxRange)
            {
                enemy.ResetFacingFlag();
                enemy.FacePlayerIfNeeded();
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (isHittingWall || isNearEdge)
            {
                enemy.IdleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.IdleState);
            }
        }

        public override void OnFixedUpdate()
        {
            isHittingWall = enemy.CheckIsHittingWall();
            isNearEdge = enemy.CheckIsNearEdge();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}