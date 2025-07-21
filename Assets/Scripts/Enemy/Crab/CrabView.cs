using ForgottonChambers.Enemy;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class CrabView : EnemyView
    {
        [SerializeField] private GameObject itemDrop;
        [SerializeField] private Transform itemDropPos;
        [SerializeField] private GameObject switchActive;

        public GameObject ItemDrop => itemDrop;
        public Transform ItemDropPos => itemDropPos;
        public GameObject SwitchActive => switchActive;

        protected override void Start()
        {
            base.Start();

            controller = new CrabController(this, enemyData, playerTransform);
        }
    }
}