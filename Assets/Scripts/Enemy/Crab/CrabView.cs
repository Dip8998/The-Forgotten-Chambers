using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class CrabView : EnemyView
    {
        protected override void Start()
        {
            base.Start();

            controller = new CrabController(this, enemyData, playerTransform);
        }
    }
}