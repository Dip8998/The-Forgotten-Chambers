using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerView playerView;
        private Rigidbody2D rb2D;

        private float comboTimer = 0f;
        private float comboResetTime = 2f;
        private int comboIndex = 0;

        private bool isPunching = false;
        private bool doubleJump = false;

        private float jumpBufferTime = 0.1f;
        private float jumpBufferCounter = 0f;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            InitializePlayerView();
        }

        private void InitializePlayerView()
        {
            playerView = Object.Instantiate(playerScriptableObject.playerPrefab);
            rb2D = playerView.GetComponent<Rigidbody2D>();
            playerView.SetPlayerController(this);
        }

        public void UpdatePlayer()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                jumpBufferCounter = jumpBufferTime;

            UpdateCombat();
        }

        public void FixedUpdatePlayer()
        {
            if (isPunching)
                return;

            UpdateMovement();

            if (jumpBufferCounter > 0)
            {
                HandleJump();
                jumpBufferCounter = 0f;
            }

            jumpBufferCounter -= Time.fixedDeltaTime;
        }

        private void UpdateMovement()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            bool isCrouchWalking = Input.GetKey(KeyCode.DownArrow) && horizontalInput != 0;

            Vector2 velocity = rb2D.linearVelocity;
            velocity.x = horizontalInput * playerScriptableObject.playerMovementSpeed;
            rb2D.linearVelocity = velocity;

            playerView.SetPlayerAnimation(horizontalInput, !IsGrounded(), isCrouchWalking);
            SetPlayerScale(horizontalInput);
        }

        private void HandleJump()
        {
            if (IsGrounded())
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, playerScriptableObject.playerJumpForce);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, playerScriptableObject.playerDoubleJumpForce);
                doubleJump = false;
                playerView.PlayAirSpinAnimation();
            }
        }

        private void UpdateCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                comboIndex++;

                if (comboIndex > 3)
                    comboIndex = 1;

                playerView.PlayPunchAnimation(comboIndex);
                comboTimer = 0f;
                isPunching = true;
            }

            if (comboIndex > 0)
            {
                comboTimer += Time.deltaTime;
                if (comboTimer > comboResetTime)
                {
                    comboIndex = 0;
                    comboTimer = 0f;
                }
            }
        }

        private void SetPlayerScale(float moveSpeed)
        {
            if (moveSpeed == 0) return;

            Vector2 scale = playerView.transform.localScale;
            scale.x = moveSpeed > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            playerView.transform.localScale = scale;
        }

        public void SetPunching(bool punching)
        {
            isPunching = punching;
        }

        private bool IsGrounded() => playerView.IsGrounded();
    }
}
