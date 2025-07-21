using ForgottonChambers.Main;
using ForgottonChambers.Player;
using UnityEngine;

namespace ForgottonChambers.KeyandDoor
{
    public class KeyView : MonoBehaviour
    {
        private Rigidbody2D rb2D;
        private BoxCollider2D boxCollider2D;

        [SerializeField] private float dropForce = 5f;
        [SerializeField] private float groundDistance = 0.1f;
        [SerializeField] private LayerMask groundMask;

        private bool hasDropped = false;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();

            boxCollider2D.enabled = false;

            rb2D.AddForce(new Vector2(Random.Range(-1f, 1f), 1f).normalized * dropForce, ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (!hasDropped && IsGrounded())
            {
                boxCollider2D.enabled = true;
                hasDropped = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!hasDropped) return;

            if (collision.TryGetComponent(out Player.PlayerView playerView))
            {
                GameService.Instance.KeyAndDoorService.OnKeyCollected();
                Destroy(gameObject);
            }
        }

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundMask);
            return hit.collider != null;
        }
    }
}
