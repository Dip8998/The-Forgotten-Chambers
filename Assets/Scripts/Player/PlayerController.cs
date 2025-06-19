using ForgottonChambers.Player.ForgottonChambers.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerController
    {
        #region State Variables
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState AirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallGrabState WallGrabState { get; private set; }
        public PlayerWallClimbState WallClimbState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        private PlayerScriptableObject playerScriptableObject;
        #endregion

        #region Components
        public PlayerInputHandler InputHandler { get; private set; }
        private Rigidbody2D rb2D;
        #endregion

        #region Player ref
        public PlayerView playerView { get; private set; }
        #endregion

        #region Vector Variables
        private Vector2 workSpace;
        public Vector2 CurrentVelocity { get; private set; }
        #endregion

        #region Other Variables
        public int FacingDirection { get; private set; }
        #endregion

        #region Unity Callback Setter functions 
        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            StateMachine = new PlayerStateMachine();
            InputHandler = new PlayerInputHandler();

            InitializePlayerView();
            InitializePlayerState();
        }

        public void SetStartPlayer()
        {
            FacingDirection = 1;
            StateMachine.InitializeState(IdleState);
        }

        public void SetUpdatePlayer()
        {
            InputHandler.UpdateInputs();
            CurrentVelocity = rb2D.linearVelocity;
            StateMachine.currentState.OnUpdate();
        }

        public void SetFixedUpdatePlayer()
        {
            StateMachine.currentState.OnFixedUpdate();
        }
        #endregion

        #region Initialization of State and Player functions 
        private void InitializePlayerState()
        {
            IdleState = new PlayerIdleState(this, StateMachine, playerScriptableObject, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, playerScriptableObject, "move");
            JumpState = new PlayerJumpState(this, StateMachine, playerScriptableObject, "inAir");
            AirState = new PlayerInAirState(this, StateMachine, playerScriptableObject, "inAir");
            LandState = new PlayerLandState(this, StateMachine, playerScriptableObject, "land");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerScriptableObject, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerScriptableObject, "wallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerScriptableObject, "wallClimb");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, playerScriptableObject, "inAir");
        }

        private void InitializePlayerView()
        {
            playerView = Object.Instantiate(playerScriptableObject.playerPrefab);
            rb2D = playerView.GetComponent<Rigidbody2D>();
            playerView.SetPlayerController(this);
        }
        #endregion

        #region Setters functions
        public void SetVelocityX(float velocity)
        {
            workSpace.Set(velocity, CurrentVelocity.y);
            rb2D.linearVelocity = workSpace;
            CurrentVelocity = workSpace;
        }

        public void SetVelocityY(float velocity)
        {
            workSpace.Set(CurrentVelocity.x, velocity);
            rb2D.linearVelocity = workSpace;
            CurrentVelocity = workSpace;
        }

        public void SetVelocity(float velocity, Vector2 angle, int dir)
        {
            angle.Normalize();
            workSpace.Set(angle.x * velocity * dir, angle.y * velocity);
            rb2D.linearVelocity = workSpace;
            CurrentVelocity = workSpace;
        }

        #endregion

        #region Check functions

        public void CheckIfShouldFlip(float xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public bool CheckIsGround() => playerView.IsGrounded();

        public bool CheckIsWall() => playerView.IsTouchingWall();

        public bool CheckIsWallBack() => playerView.IsTouchingWallBack();

        #endregion

        #region Other Functions


        private void Flip()
        {
            FacingDirection *= -1;
            playerView.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        public void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();

        public void AnimationFinishedTrigger() => StateMachine.currentState.AnimationFinishTrigger();

        #endregion
    }
}