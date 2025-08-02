using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Sound;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyMeleeAttackState : EnemyAttackState
    {
        public EnemyMeleeAttackState(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyScriptableObject enemyData, int animBoolHash) : base(stateMachine, enemyController, enemyData, animBoolHash)
        {
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

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void AnimationAttackTrigger()
        {
            base.AnimationAttackTrigger();

            switch (enemyData.enemyType)
            {
                case EnemyType.CrawlerCrab:
                    GameService.Instance.SoundService.Play(Sounds.CRABENEMYATTACK);
                    break;
                case EnemyType.GroundedSkeleton:
                    GameService.Instance.SoundService.Play(Sounds.SKELETONATTACK);
                    break;
                case EnemyType.Boss:
                    GameService.Instance.SoundService.Play(Sounds.BOSSMELEEATTACK);
                    break;
            }

            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(enemy.Enemy.AttackPosition.position, enemyData.attackRadius, enemyData.playerLayer);

            foreach (Collider2D obj in detectedObjects)
            {
                PlayerController Player = GameService.Instance.PlayerService.GetPlayerController();
                Player.Damage(enemyData.attackDamage);
                
                GameService.Instance.ParticleService.PlayParticle(ParticleType.PlayerHit, Player.PlayerView.transform.position, Quaternion.identity);
            }
        }

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }
    }
}