using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyAttackState : EnemyState
    {
        protected bool isAnimationFinished;
        protected bool isPlayerInMinRange;
        protected bool isPlayerInMaxRange;

        public EnemyAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName) : base(stateMachine, enemyController, enemyData, animBoolName)
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
            isPlayerInMinRange = enemy.CheckIsPlayerInMinRange();
            isPlayerInMaxRange = enemy.CheckIsPlayerInMaxRange();
            isAnimationFinished = false;
            enemy.SetVelocity(0f);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public virtual void AnimationAttackTrigger()
        {

        }

        public virtual void AnimationFinishedTrigger()
        {
            isAnimationFinished = true;
        }
    }
}