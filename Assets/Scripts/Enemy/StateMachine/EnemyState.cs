using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyState 
    {
        protected EnemyStateMachine stateMachine;
        protected EnemyController enemy;
        protected EnemyScriptableObject enemyData;
        protected float startTime;
        protected string animBoolName;

        public EnemyState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, string animBoolName)
        {
            this.stateMachine = stateMachine;
            this.enemy = enemyController;
            this.animBoolName = animBoolName;
            this.enemyData = enemyData;
        }

        public virtual void OnStateEnter()
        {
            startTime = Time.time;
            enemy.EnemyView.EnemyAnimator.SetBool(animBoolName, true);
        }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }
 
        public virtual void OnStateExit()
        {
            enemy.EnemyView.EnemyAnimator.SetBool(animBoolName, false);
        }
    }
}
