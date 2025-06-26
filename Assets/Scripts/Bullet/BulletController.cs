using UnityEngine;

namespace ForgottonChambers.Bullets
{
    public class BulletController
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletData;
        private BulletPool bulletPool;

        public BulletView View => bulletView; 

        public BulletController(BulletView view, BulletScriptableObject data, BulletPool pool)
        {
            bulletView = view;
            bulletData = data;
            bulletPool = pool;

            bulletView.SetController(this);
        }

        public void Shoot(Vector2 direction, Vector3 position)
        {
            bulletView.gameObject.SetActive(true);
            bulletView.transform.position = position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            bulletView.transform.rotation = Quaternion.Euler(0, 0, angle);

            bulletView.SetVelocity(direction.normalized * bulletData.speed);
        }

        public void ReturnToPool()
        {
            bulletView.gameObject.SetActive(false);
            bulletPool.ReturnBullet(this);
        }

        public int GetDamage() => bulletData.damage;
        public float GetLifetime() => bulletData.destroyingTime;
    }
}
