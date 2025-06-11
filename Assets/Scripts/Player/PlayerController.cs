using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

        public void FixedUpdatePlayer()=> UpdateMovement();

        public void UpdatePlayer() => UpdateCombat();

        private void UpdateMovement()
        {
            if (isPunching)
                return;

            float hMove = Input.GetAxisRaw("Horizontal");
            bool jump = Input.GetKey(KeyCode.Space);
            bool isCrouchWalk = Input.GetKey(KeyCode.DownArrow) && hMove != 0;

            PlayerMovement(hMove, jump, isCrouchWalk);
            PlayerJump(jump);
        }

        private void PlayerJump(bool jump)
        {
            if(jump && IsGrounded())
            {
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, playerScriptableObject.playerJumpForce);
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

        private void PlayerMovement(float hMove, bool jump, bool isCrouchWalking)
        {
            Vector2 velocity = rb2D.linearVelocity;
            velocity.x = hMove * playerScriptableObject.playerMovementSpeed;
            rb2D.linearVelocity = velocity;

            playerView.SetPlayerAnimation(hMove, !IsGrounded(), isCrouchWalking);
            SetPlayerScale(hMove);
        }

        private void SetPlayerScale(float moveSpeed)
        {
            Vector2 scale = playerView.transform.localScale;

            if (moveSpeed < 0)
            {
                scale.x = -1f * Mathf.Abs(scale.x);
            }
            else if (moveSpeed > 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            playerView.transform.localScale = scale;
        }

        public void SetPunching(bool punching)
        {
            isPunching = punching;
        }

        private bool IsGrounded() => playerView.IsGrounded();
    }
}
