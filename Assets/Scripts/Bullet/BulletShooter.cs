using UnityEngine;

namespace ForgottonChambers.Bullets
{
    public class BulletShooter : MonoBehaviour
    {
        [SerializeField] private BulletView bulletPrefab;
        [SerializeField] private BulletScriptableObject bulletData;
        [SerializeField] private Transform firePoint;

        private BulletPool bulletPool;

        private void Start()
        {
            bulletPool = new BulletPool(bulletPrefab, bulletData, 8);
        }

        public void Shoot()
        {
            var bullet = bulletPool.GetBullet();
            Vector2 dir = transform.right * Mathf.Sign(transform.localScale.x);
            bullet.Shoot(dir, firePoint.position);
        }
    }
}
