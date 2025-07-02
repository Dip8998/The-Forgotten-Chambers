using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class CrabController : EnemyController
    {
        public CrabController(EnemyView enemyView, EnemyScriptableObject enemyData, Transform playerTransform)
            : base(enemyView, enemyData, playerTransform)
        {
        }

        protected override void InitializeStates()
        {
            base.InitializeStates();
        }
    }
}