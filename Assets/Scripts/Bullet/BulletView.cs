using ForgottonChambers.Main;
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
        }

        private void OnEnable()
        {
            timer = 0f;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= controller.GetLifetime())
            {
                controller.ReturnToPool();
            }
        }

        public void SetController(BulletController bulletController)
        {
            controller = bulletController;
        }

        public void SetVelocity(Vector2 velocity)
        {
            rb.linearVelocity = velocity;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            controller.ReturnToPool();

            if(collision.TryGetComponent(out PlayerView player))
            {
                player.PlayerController.Damage(controller.GetDamage());
                GameService.Instance.ParticleService.PlayParticle(Particles.ParticleType.FireHit, player.transform.position, Quaternion.identity);
            }
            else if(collision.TryGetComponent(out EnemyView enemyView))
            {
                enemyView.Controller.Damage(controller.GetDamage());

            }
        }
    }
}
