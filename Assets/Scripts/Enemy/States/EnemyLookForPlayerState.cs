using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyLookForPlayerState : EnemyState
    {
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;
        protected bool isAllTurnsDone;
        protected bool isAllTurnsTimeDone;
        protected bool turnImmediately;
        protected bool hasMoved;

        protected float lastTurnTime;

        protected int amountOfTurnsDone;


        public EnemyLookForPlayerState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            isAllTurnsDone = false;
            isAllTurnsTimeDone = false;
            turnImmediately = false;
            lastTurnTime = startTime;
            amountOfTurnsDone = 0;
            hasMoved = false;

            enemy.SetVelocity(0);
        }

        public override void OnUpdate()
        {
            if (turnImmediately)
            {
                enemy.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
                turnImmediately = false;
            }
            else if (Time.time >= lastTurnTime + enemyData.timeBetweenTurns && !isAllTurnsDone)
            {
                enemy.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
            }

            if (amountOfTurnsDone >= enemyData.amountOfTurns)
                isAllTurnsDone = true;

            if (isAllTurnsDone && Time.time >= lastTurnTime + enemyData.timeBetweenTurns)
                isAllTurnsTimeDone = true;

            if (isPlayerInMinRange || isPlayerInMaxRange)
            {
                enemy.FacePlayerIfNeeded();
                stateMachine.ChangeState(enemy.PlayerDetectedState);
            }
            else if (isAllTurnsTimeDone && !hasMoved)
            {
                hasMoved = true;
                stateMachine.ChangeState(enemy.MoveState);
            }
        }

        public override void OnFixedUpdate()
        {
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public void SetTurnImmediately(bool flip)
        {
            turnImmediately = flip;
        }
    }
}