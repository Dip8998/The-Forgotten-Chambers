using Unity.VisualScripting;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask wallLayer;
 

        public Animator playerAnimator {  get; private set; }
        private PlayerController playerController;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerController.SetStartPlayer();
        }

        private void Update()
        {
            playerController.SetUpdatePlayer();
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

        public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        public bool IsTouchingWall() => Physics2D.Raycast(wallCheck.position, Vector2.right * playerController.FacingDirection, wallCheckDistance, wallLayer);

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            if (wallCheck != null)
            {
                Gizmos.color = Color.blue; 
                Vector2 rayDirection = Vector2.right * (playerController != null ? playerController.FacingDirection : 1);
                Gizmos.DrawRay(wallCheck.position, rayDirection * wallCheckDistance);
            }
        }
    }
}