using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class SkeletonView : EnemyView
    {
        [SerializeField] protected GameObject itemDrop;
        [SerializeField] protected Transform itemDropPos;

        public Transform ItemDropPos => itemDropPos;
        public GameObject ItemDrop => itemDrop;

        protected override void Start()
        {
            base.Start();

            controller = new SkeletonController(this, enemyData, playerTransform);
        }
    }
}