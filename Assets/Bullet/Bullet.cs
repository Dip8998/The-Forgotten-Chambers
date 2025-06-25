using UnityEngine;
using ForgottonChambers.HealthSystem;

namespace ForgottonChambers.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
