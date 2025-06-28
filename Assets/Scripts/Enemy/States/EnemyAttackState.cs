using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyAttackState : EnemyState
    {
        protected bool isAnimationFinished;
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;

        public EnemyAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            isAnimationFinished = false;
            enemy.SetVelocity(0f);
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
        }

        public virtual void AnimationAttackTrigger()
        {
        }

        public virtual void AnimationFinishedTrigger()
        {
            isAnimationFinished = true;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}