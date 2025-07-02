using UnityEngine;
using ForgottonChambers.Main;

namespace ForgottonChambers.Bullets
{
    public class BulletShooter : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;


        public void Shoot()
        {
            BulletController bullet = GameService.Instance.BulletService.GetBullet();

            if (bullet != null)
            {
                Vector2 dir = transform.right * Mathf.Sign(transform.localScale.x);
                bullet.Shoot(dir, firePoint.position);
            }
            else
            {
                Debug.LogWarning("BulletShooter: Failed to get a bullet. Check BulletService initialization.");
            }
        }
    }
}