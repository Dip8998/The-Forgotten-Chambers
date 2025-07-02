using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class SkeletonController : EnemyController
    {
        public SkeletonController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {
        }

        protected override void InitializeStates()
        {
            base.InitializeStates();
        }
    }
}