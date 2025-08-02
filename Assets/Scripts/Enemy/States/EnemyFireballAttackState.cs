using ForgottonChambers.Main;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyFireballAttackState : EnemyAttackState
    {
        private IFireballShooter fireballShooter;

        public EnemyFireballAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash)
            : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
            fireballShooter = enemyController as IFireballShooter;
            if (fireballShooter == null)
            {
                Debug.LogError($"{enemyController.GetType().Name} does not implement IFireballShooter");
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemy.SetVelocity(0f);
        }

        public override void OnUpdate()
        {
            if (isAnimationFinished)
            {
                if (isPlayerInMinRange || isPlayerInMaxRange)
                {
                    stateMachine.ChangeState(enemy.PlayerDetectedState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.LookForPlayerState);
                }
            }
        }

        public override void AnimationAttackTrigger()
        {
            base.AnimationAttackTrigger();
            Debug.Log("Fireball Attack Trigger");
            fireballShooter?.ShootFireball();
            GameService.Instance.SoundService.Play(Sound.Sounds.FIREWORMATTACK);
        }
    }
}