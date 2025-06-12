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
        }

        private void Update()
        {
            playerController?.UpdatePlayer();
        }

        public void SetPlayerController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        private void FixedUpdate()
        {
            playerController?.FixedUpdatePlayer();
        }

        public void SetPlayerAnimation(float moveSpeed, bool isJumping, bool isCrouchWalking)
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("RunSpeed", Mathf.Abs(moveSpeed));
                playerAnimator.SetBool("Jump", isJumping);
                playerAnimator.SetBool("CrouchWalk", isCrouchWalking);
            }
        }

        public void PlayPunchAnimation(int punchIndex)
        {
            if (playerAnimator != null)
            {
                switch (punchIndex)
                {
                    case 1:
                        playerAnimator.SetTrigger("Punch1");
                        break;
                    case 2:
                        playerAnimator.SetTrigger("Punch2");
                        break;
                    case 3:
                        playerAnimator.SetTrigger("Punch3");
                        break;
                }
            }
        }

        public void PlayAirSpinAnimation()
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("AirSpin");
            }
        }

        public void OnPunchAnimationEnd()
        {
            playerController.SetPunching(false);
        }


        public bool IsGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        private void OnDrawGizmos()
        {
            if(groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
