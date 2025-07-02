using ForgottonChambers.Bullets;
using ForgottonChambers.Main;
using ForgottonChambers.Particles;
using ForgottonChambers.Player;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 3f;

    private Rigidbody2D rb;
    private int facingDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int direction, float projectileSpeed, int projectileDamage, float projectileLifetime)
    {
        facingDirection = direction;
        speed = projectileSpeed;
        damage = projectileDamage;
        lifetime = projectileLifetime;

        rb.linearVelocity = new Vector2(speed * facingDirection, 0f);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerView playerView))
        {
            GameService.Instance.PlayerService.GetPlayerController().Damage(damage);
            GameService.Instance?.ParticleService?.PlayParticle(ParticleType.FireHit, playerView.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
