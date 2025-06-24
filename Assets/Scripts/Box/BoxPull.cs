using UnityEngine;

namespace ForgottonChambers.Box
{
    public class boxpull : MonoBehaviour
    {
        public float defaultMass = 5f;
        public float imovableMass = 5000f;
        public bool beingPushed = false;

        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
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
                rb.mass = imovableMass;
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
