using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.Bullets
{
    public class BulletPool
    {
        private BulletView bulletPrefab;
        private BulletScriptableObject bulletData;
        private Transform poolParent;

        private readonly List<BulletController> bullets = new();

        public BulletPool(BulletView bulletPrefab, BulletScriptableObject bulletData, int count, Transform parent = null)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletData = bulletData;
            this.poolParent = parent;

            for (int i = 0; i < count; i++)
            {
                CreateNewBullet();
            }
        }

        private BulletController CreateNewBullet()
        {
            BulletView view = GameObject.Instantiate(bulletPrefab, poolParent);
            view.gameObject.SetActive(false);

            var controller = new BulletController(view, bulletData, this);
            bullets.Add(controller);
            return controller;
        }

        public BulletController GetBullet()
        {
            foreach (var bullet in bullets)
            {
                if (!bullet.View.gameObject.activeInHierarchy)
                    return bullet;
            }

            return CreateNewBullet();
        }

        public void ReturnBullet(BulletController bullet)
        {
            // Deactivation is already handled in controller
        }
    }
}
