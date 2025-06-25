using ForgottonChambers.Weapons;
using UnityEngine;

namespace ForgottonChambers.Bullets
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float bulletSpeed;

        public void ShootBullet()
        {
            if (bulletPrefab != null)
            {
                Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                Bullet bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.linearVelocity = direction * bulletSpeed;
                }
            }
        }
    }
}
