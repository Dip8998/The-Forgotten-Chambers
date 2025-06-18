using Unity.VisualScripting;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

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

        public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}