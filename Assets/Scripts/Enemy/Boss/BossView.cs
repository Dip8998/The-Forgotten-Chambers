using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class BossView : EnemyView
    {
        public Transform[] crabSummonPoints;
        public Transform firePosition;

        protected override void Start()
        {
            enemyAnimator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            baseScale = transform.localScale;

            controller = new BossController(this, enemyData, playerTransform);
        }
    }
}
