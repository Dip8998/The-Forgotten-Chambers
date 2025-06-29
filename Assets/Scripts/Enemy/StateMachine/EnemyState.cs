using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyState
    {
        public static readonly int ANIM_IDLE = Animator.StringToHash("Idle");
        public static readonly int ANIM_MOVE = Animator.StringToHash("Move");
        public static readonly int ANIM_PLAYER_DETECTED = Animator.StringToHash("PlayerDetected");
        public static readonly int ANIM_CHARGE = Animator.StringToHash("Charge");
        public static readonly int ANIM_LOOK_FOR_PLAYER = Animator.StringToHash("LookForPlayer");
        public static readonly int ANIM_MELEE_ATTACK = Animator.StringToHash("MeleeAttack");

        protected EnemyStateMachine stateMachine;
        protected EnemyController enemy;
        protected EnemyScriptableObject enemyData;
        protected float startTime;
        protected int animBoolHash;

        public EnemyState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
        {
            this.stateMachine = stateMachine;
            this.enemy = enemyController;
            this.animBoolHash = animBoolHash;
            this.enemyData = enemyData;
        }

        public virtual void OnStateEnter()
        {
            startTime = Time.time;
            enemy.Enemy.EnemyAnimator.SetBool(animBoolHash, true);
        }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnStateExit()
        {
            enemy.Enemy.EnemyAnimator.SetBool(animBoolHash, false);
        }
    }
}