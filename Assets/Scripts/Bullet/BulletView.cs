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
        }
    }
}
