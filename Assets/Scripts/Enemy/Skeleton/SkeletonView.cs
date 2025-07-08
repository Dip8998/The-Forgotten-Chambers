using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class SkeletonView : EnemyView
    {
        protected override void Start()
        {
            base.Start();

            controller = new SkeletonController(this, enemyData, playerTransform);
        }
    }
}