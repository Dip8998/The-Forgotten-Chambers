using UnityEngine;

namespace ForgottonChambers.Box
{
    public class BoxController : MonoBehaviour
    {
        public float defaultMass = 5f;
        public bool beingPushed = false;

        private Rigidbody2D rb;

        void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        void FixedUpdate()
        {
            if (beingPushed)
            {
                rb.mass = defaultMass;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rb.mass = defaultMass;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
