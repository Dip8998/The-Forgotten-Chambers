using ForgottonChambers.Bullets;
using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

public class FireWormView : EnemyView
{
    protected override void Start()
    {
        base.Start();

        controller = new FireWormController(this, enemyData, playerTransform);
    }
}