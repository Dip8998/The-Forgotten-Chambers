using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerView : MonoBehaviour
    {
        [Header("Checks")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask wallLayer;

        [SerializeField] private Transform boxCheck;
        [SerializeField] private float boxCheckDistance;
        [SerializeField] private LayerMask boxLayer;

        [SerializeField] private Transform ceilingCheck;
        [SerializeField] private float ceilingCheckDistance;

        public Animator playerAnimator { get; private set; }
        private PlayerController playerController;
        private GameObject box;
        private Rigidbody2D playerRb;

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
            playerRb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            playerController.SetStartPlayer();
        }

        private void Update()
        {
            playerController.SetUpdatePlayer();
            HandleBoxInteraction();
        }

        private void FixedUpdate()
        {
            playerController.SetFixedUpdatePlayer();
        }

        public void SetPlayerController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void OnAnimationFinished()
        {
            playerController.AnimationFinishedTrigger();
        }

        public bool IsCeiling() =>
            Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, groundLayer);

        public bool IsGrounded() =>
            Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        public bool IsTouchingWall() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * playerController.FacingDirection, wallCheckDistance, wallLayer);

        public bool IsTouchingWallBack() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * -playerController.FacingDirection, wallCheckDistance, wallLayer);

        private void HandleBoxInteraction()
        {
            Physics2D.queriesStartInColliders = false;

            if (!playerController.InputHandler.BoxPushPullInput || playerController.InputHandler.BoxDropeInput)
            {
                if (box != null)
                {
                    if (box.TryGetComponent(out FixedJoint2D joint))
                        joint.enabled = false;

                    if (box.TryGetComponent(out Rigidbody2D rb))
                    {
                        rb.linearVelocity = Vector2.zero;
                        rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    }

                    box = null; 
                }
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(boxCheck.position, Vector2.right * playerController.FacingDirection, boxCheckDistance, boxLayer);
            if (hit.collider != null && hit.collider.CompareTag("Box"))
            {
                box = hit.collider.gameObject;

                if (box.TryGetComponent(out FixedJoint2D joint) &&
                    box.TryGetComponent(out Rigidbody2D rb))
                {
                    joint.enabled = true;
                    joint.connectedBody = playerRb;

                    rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
                }
            }
        }

        public bool HasBoxAttached() => box != null;

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            if (ceilingCheck != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * ceilingCheckDistance);
            }

            if (wallCheck != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(wallCheck.position, Vector2.right * wallCheckDistance);
                Gizmos.DrawRay(wallCheck.position, Vector2.left * wallCheckDistance);
            }

            if (boxCheck != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(boxCheck.position, Vector2.right * boxCheckDistance);
                Gizmos.DrawRay(boxCheck.position, Vector2.left * boxCheckDistance);
            }
        }
    }
}
