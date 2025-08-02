using ForgottonChambers.Main;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using System.Collections.Generic;

namespace ForgottonChambers.Enemy
{
    public class BossController : EnemyController, IFireballShooter
    {
        public EnemyFireballAttackState FireballAttackState { get; private set; }
        private float lastFireballTime;
        private bool hasSummonedCrabs = false;
        private Transform[] summonPoints;
        private Transform firePoint;

        private List<GameObject> summonedCrabs = new List<GameObject>();

        public BossController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {
            BossView bossView = enemyView as BossView;
            if (bossView != null)
            {
                summonPoints = bossView.CrabSummonPoints;
                firePoint = bossView.FirePosition;
            }
        }

        protected override void InitializeStates()
        {
            base.InitializeStates();

            FireballAttackState = new EnemyFireballAttackState(stateMachine, this, enemyData, EnemyState.ANIM_FIREBALL_ATTACK);
            PlayerDetectedState = new BossPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);
        }

        public override void Damage(int damage, Vector2 hitSourcePosition, bool isBullet)
        {
            base.Damage(damage, hitSourcePosition);

            if (!hasSummonedCrabs && CurrentHealth <= enemyData.enemyHealth / 2)
            {
                hasSummonedCrabs = true;
                SummonCrabs();
            }
        }

        private void SummonCrabs()
        {
            if (enemyData.crabPrefab == null) return;

            Vector3 bossPos = Enemy.transform.position;
            Vector3 leftOffset = bossPos + new Vector3(-2f, 0f, 0f);
            Vector3 rightOffset = bossPos + new Vector3(2f, 0f, 0f);

            Transform dropParent = GameService.Instance.LevelService.CurrentLevelParent?.transform;

            GameObject crab1 = Object.Instantiate(enemyData.crabPrefab, leftOffset, Quaternion.identity);
            if (dropParent != null) crab1.transform.SetParent(dropParent);
            GameObject crab2 = Object.Instantiate(enemyData.crabPrefab, rightOffset, Quaternion.identity);
            if (dropParent != null) crab2.transform.SetParent(dropParent);

            Debug.Log("Crab 1 spawned at: " + leftOffset);
            Debug.Log("Crab 2 spawned at: " + rightOffset);

            summonedCrabs.Add(crab1);
            summonedCrabs.Add(crab2);

            InitializeCrab(crab1);
            InitializeCrab(crab2);
        }


        private void InitializeCrab(GameObject crabGO)
        {
            CrabView crabView = crabGO.GetComponent<CrabView>();
            if (crabView == null) return;

            crabView.EnemyData.maxPlayerDetectedDistance = 30;
            Transform playerTransform = GameService.Instance.PlayerService.GetPlayerController()?.PlayerView.transform;

            if (enemyData != null && playerTransform != null)
            {
                EnemyController controller = new EnemyController(crabView, enemyData, playerTransform);
                crabView.SetController(controller);
            }
        }

        public override void Die()
        {
            base.Die();

            foreach (var crab in summonedCrabs)
            {
                if (crab != null)
                {
                    GameObject.Destroy(crab);
                }
            }

            summonedCrabs.Clear();
        }

        public bool CanShootFireball()
        {
            bool cooldownReady = Time.time >= lastFireballTime + enemyData.fireballCooldown;
            bool notInKnockback = stateMachine.CurrentState != KnockbackState;
            return cooldownReady && notInKnockback;
        }

        public void ShootFireball()
        {
            Debug.Log("Boss shoots fireball!");

            if (CanShootFireball())
            {
                GameService.Instance.SoundService.Play(Sound.Sounds.BOSSRANGEATTACK);
                lastFireballTime = Time.time;
                Transform dropParent = GameService.Instance.LevelService.CurrentLevelParent?.transform;
                GameObject fireballGO = GameObject.Instantiate(enemyData.fireballPrefab, firePoint.transform.position, Quaternion.identity);
                if (dropParent != null) fireballGO.transform.SetParent(dropParent);
                FireballProjectile fireball = fireballGO.GetComponent<FireballProjectile>();
                if (fireball != null)
                {
                    fireball.Initialize(FacingDirection, enemyData.fireballSpeed, enemyData.fireballDamage, fireball.lifetime);
                }
            }
        }
    }
}
