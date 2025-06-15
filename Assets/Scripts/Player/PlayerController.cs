using UnityEngine;
using ForgottonChambers.StateMachine;

namespace ForgottonChambers.Player
{
    public class PlayerController
    {
        public PlayerScriptableObject playerScriptableObject {  get; private set; }
        public PlayerView playerView { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
       
        private Rigidbody2D rb2D;

        private bool isPunching = false;
        public bool IsPunching => isPunching;

        private bool doubleJump = false;
        public bool CanDoubleJump => doubleJump;

        private bool hasSword;
        public bool HasSword => hasSword;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            InitializePlayerView();
            InputHandler = new PlayerInputHandler();
            StateMachine = new PlayerStateMachine(this);
            StateMachine.Initialize(PlayerState.Idle);
        }

        private void InitializePlayerView()
        {
            playerView = Object.Instantiate(playerScriptableObject.playerPrefab);
            rb2D = playerView.GetComponent<Rigidbody2D>();
            playerView.SetPlayerController(this);
        }

        public void UpdatePlayer()
        {
            InputHandler.UpdateInputs();
            StateMachine.UpdateState();
        }

        public void FixedUpdatePlayer()
        {
           StateMachine.FixedUpdateState();
        }

        public void ApplyMovement(float horizontalInput)
        {
            Vector2 velocity = rb2D.linearVelocity;
            velocity.x = horizontalInput * playerScriptableObject.playerMovementSpeed;
            rb2D.linearVelocity = velocity;
        }

        public void ApplyJumpForce(float force)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, force);
            InputHandler.ResetJumpBuffer();
        }

        public void SetHasSword(bool value)
        {
            hasSword = value;
            playerView.playerAnimator.SetBool("HasSword", hasSword);
        }

        public void SetPunching(bool punching)
        {
            isPunching = punching;
        }

        public void ResetDoubleJumpAbility()
        {
            doubleJump = true;
        }

        public void DisableDoubleJumpAbility()
        {
            doubleJump = false;
        }

        public void SetPlayerScale(float moveSpeed)
        {
            if (moveSpeed == 0) return;

            Vector2 scale = playerView.transform.localScale;
            scale.x = moveSpeed > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            playerView.transform.localScale = scale;
        }

        public bool IsGrounded() => playerView.IsGrounded();
    }
}
