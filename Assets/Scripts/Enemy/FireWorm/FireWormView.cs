// === FireWormView.cs ===
using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class FireWormView : EnemyView
    {
        [SerializeField] private EnemyScriptableObject fireWormData;

        protected override void Start()
        {
            base.Start();

            controller = new FireWormController(this, fireWormData, playerTransform);
        }
    }
}
