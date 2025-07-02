using ForgottonChambers.Enemy;
using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using UnityEngine;

namespace ForgottonChambers.Bullets
{
    public class BulletView : MonoBehaviour
    {
        private Rigidbody2D rb;
        private BulletController controller;
        private float timer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("BulletView: Rigidbody2D not found on this GameObject!", this);
            }
        }

        private void OnEnable()
        {
            timer = 0f;
        }

        private void Update()
        {
            if (controller != null)
            {
                timer += Time.deltaTime;
                if (timer >= controller.GetLifetime())
                {
                    controller.ReturnToPool();
                }
            }
        }

        public void SetController(BulletController bulletController)
        {
            controller = bulletController;
        }

        public void SetVelocity(Vector2 velocity)
        {
            if (rb != null)
            {
                rb.linearVelocity = velocity;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (controller == null)
            {
                Debug.LogWarning("BulletView: Collision detected but controller is null. Destroying GameObject.", this);
                Destroy(gameObject);
                return;
            }

            controller.ReturnToPool();

            if (collision.TryGetComponent(out EnemyView enemyView))
            {
                enemyView.Controller.Damage(controller.GetDamage(), transform.position);
                GameService.Instance?.ParticleService?.PlayParticle(ParticleType.EnemyHit, enemyView.transform.position, Quaternion.identity);
            }
        }
    }
}