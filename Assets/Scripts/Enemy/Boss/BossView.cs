using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class BossView : EnemyView
    {
        [SerializeField] private Transform[] crabSummonPoints;
        [SerializeField] private Transform firePosition;
        [SerializeField] private GameObject itemDrop;
        [SerializeField] private Transform itemDropPos;

        public Transform[] CrabSummonPoints => crabSummonPoints;
        public Transform FirePosition => firePosition;
        public GameObject ItemDrop => itemDrop;
        public Transform ItemDropPos => itemDropPos;


        protected override void Start()
        {
            enemyAnimator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            baseScale = transform.localScale;

            controller = new BossController(this, enemyData, playerTransform);
        }
    }
}
