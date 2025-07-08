using ForgottonChambers.Main;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class BossController : EnemyController, IFireballShooter
    {
        public EnemyFireballAttackState FireballAttackState { get; private set; }
        private float lastFireballTime;
        private bool hasSummonedCrabs = false;
        private Transform[] summonPoints;
        private Transform firePoint;

        public BossController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {

            BossView bossView = enemyView as BossView;
            if (bossView != null)
            {
                summonPoints = bossView.crabSummonPoints;
                firePoint = bossView.firePosition;
            }

        }

        protected override void InitializeStates()
        {
            base.InitializeStates();

            FireballAttackState = new EnemyFireballAttackState(stateMachine, this, enemyData, EnemyState.ANIM_FIREBALL_ATTACK);
            PlayerDetectedState = new BossPlayerDetectedState(stateMachine, this, enemyData, EnemyState.ANIM_PLAYER_DETECTED);
        }

        public override void Damage(int damage, Vector2 hitSourcePosition)
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

            GameObject crab1 = Object.Instantiate(enemyData.crabPrefab, leftOffset, Quaternion.identity);
            Debug.Log("Crab 1 spawned at: " + leftOffset);

            GameObject crab2 = Object.Instantiate(enemyData.crabPrefab, rightOffset, Quaternion.identity);
            Debug.Log("Crab 2 spawned at: " + rightOffset);

            InitializeCrab(crab1);
            InitializeCrab(crab2);
        }


        private void InitializeCrab(GameObject crabGO)
        {
            EnemyView crabView = crabGO.GetComponent<EnemyView>();
            if (crabView == null) return;

            Transform playerTransform = GameService.Instance.PlayerService.GetPlayerController()?.PlayerView.transform;

            if (enemyData != null && playerTransform != null)
            {
                EnemyController controller = new EnemyController(crabView, enemyData, playerTransform);
                crabView.SetController(controller);
            }
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
                lastFireballTime = Time.time;
                GameObject fireballGO = GameObject.Instantiate(enemyData.fireballPrefab, firePoint.transform.position, Quaternion.identity);
                FireballProjectile fireball = fireballGO.GetComponent<FireballProjectile>();
                if (fireball != null)
                {
                    fireball.Initialize(FacingDirection, enemyData.fireballSpeed, enemyData.fireballDamage, fireball.lifetime);
                }
            }
        }
    }
}
