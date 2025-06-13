using Unity.Burst.Intrinsics;
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

        public void SetPlayerAnimation(float moveSpeed, bool isJumping, bool isCrouchWalking, bool isCrouchIdle)
        {
            if (playerAnimator != null)
            {
                if (playerAnimator.GetBool("Jump") != isJumping)
                    Debug.Log($"🔄 Jump Set To: {isJumping} in state: {playerController?.StateMachine?.ToString()}");

                playerAnimator.SetFloat("RunSpeed", Mathf.Abs(moveSpeed));

                if (playerAnimator.GetBool("Jump") != isJumping)
                    playerAnimator.SetBool("Jump", isJumping);

                playerAnimator.SetBool("CrouchWalk", isCrouchWalking);
                playerAnimator.SetBool("CrouchIdle", isCrouchIdle);
            }
        }

        public void PlayPunchAnimation()
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Punch");
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

        public bool IsFalling() => playerAnimator.GetComponent<Rigidbody2D>().linearVelocity.y < 0 && !IsGrounded();


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
