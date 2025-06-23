using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Weapons;
using System.Linq;

namespace ForgottonChambers.Player
{
    public class PlayerController
    {
        #region State Machine
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
        public PlayerCrouchIdleState CrouchIdleState { get; private set; }
        public PlayerCrouchMoveState CrouchMoveState { get; private set; }
        public PlayerAttackState AttackState { get; private set; }
        #endregion

        #region Dependencies & Components
        private readonly PlayerScriptableObject playerConfig;
        public PlayerInputHandler InputHandler { get; private set; }
        public PlayerView PlayerView { get; private set; }
        public BoxCollider2D MovementCollider { get; private set; }

        private Rigidbody2D _rb2D;
        private Vector2 _workSpace;
        public Vector2 CurrentVelocity => _rb2D.linearVelocity;
        #endregion

        #region Other Variables
        public int FacingDirection { get; private set; } = 1;
        private int _currentWeaponIndex;
        #endregion

        #region Player call back functions
        public PlayerController(PlayerScriptableObject playerConfig)
        {
            this.playerConfig = playerConfig;
            StateMachine = new PlayerStateMachine();
            InputHandler = new PlayerInputHandler();
            _workSpace = Vector2.zero;

            InitializePlayerView();
            InitializePlayerStates();
        }

        public void SetupPlayer()
        {
            MovementCollider = PlayerView.GetComponent<BoxCollider2D>();
            _currentWeaponIndex = 0;
            AttackState.SetWeapon(PlayerView.Weapons[_currentWeaponIndex]);

            StateMachine.InitializeState(IdleState);
        }

        public void OnPlayerUpdate()
        {
            InputHandler.UpdateInputs();
            StateMachine.currentState.OnUpdate();
            if (InputHandler.SwitchWeaponInput)
            {
                SwitchWeapon();
            }
        }

        public void OnPlayerFixedUpdate()
        {
            StateMachine.currentState.OnFixedUpdate();
        }
        #endregion

        #region Initialization

        private void InitializePlayerView()
        {
            if (playerConfig.playerPrefab == null)
            {
                return;
            }
            PlayerView = Object.Instantiate(playerConfig.playerPrefab);
            _rb2D = PlayerView.GetComponent<Rigidbody2D>();
            PlayerView.SetPlayerController(this);
        }

        private void InitializePlayerStates()
        {
            IdleState = new PlayerIdleState(this, StateMachine, playerConfig, "idle");
            MoveState = new PlayerMoveState(this, StateMachine, playerConfig, "move");
            JumpState = new PlayerJumpState(this, StateMachine, playerConfig, "inAir");
            AirState = new PlayerInAirState(this, StateMachine, playerConfig, "inAir");
            LandState = new PlayerLandState(this, StateMachine, playerConfig, "land");
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerConfig, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerConfig, "wallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerConfig, "wallClimb");
            WallJumpState = new PlayerWallJumpState(this, StateMachine, playerConfig, "inAir");
            CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerConfig, "crouchIdle");
            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerConfig, "crouchMove");

            string attackAnimBool = "attack";
            AttackState = new PlayerAttackState(this, StateMachine, playerConfig, attackAnimBool);
        }

        #endregion

        #region Movement and Velocity Application

        public void SetVelocityZero()
        {
            _rb2D.linearVelocity = Vector2.zero;
        }

        public void SetVelocityX(float velocity)
        {
            _workSpace.Set(velocity, _rb2D.linearVelocity.y);
            ApplyVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            _workSpace.Set(_rb2D.linearVelocity.x, velocity);
            ApplyVelocity();
        }

        public void SetVelocity(float speed, Vector2 angle, int dir)
        {
            angle.Normalize();
            _workSpace.Set(angle.x * speed * dir, angle.y * speed);
            ApplyVelocity();
        }

        private void ApplyVelocity()
        {
            _rb2D.linearVelocity = _workSpace;
        }

        #endregion

        #region Checks & Utilities

        public void CheckIfShouldFlip(float xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        private void Flip()
        {
            FacingDirection *= -1;
            PlayerView.transform.Rotate(0f, 180f, 0f);
        }

        public bool CheckIsGround() => PlayerView.IsGrounded();

        public bool CheckIsWall() => PlayerView.IsTouchingWall();

        public bool CheckIsWallBack() => PlayerView.IsTouchingWallBack();

        public bool CheckIsCeiling() => PlayerView.IsCeiling();

        public void SetColliderSize(Vector2 newSize, Vector2 newOffset)
        {
            if (MovementCollider != null)
            {
                MovementCollider.size = newSize;
                MovementCollider.offset = newOffset;
            }
            else
            {
            }
        }

        public void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();

        public void AnimationFinishedTrigger() => StateMachine.currentState.AnimationFinishTrigger();

        #endregion

        #region Other Functions
        private void SwitchWeapon()
        {
            PlayerView.Weapons[_currentWeaponIndex].gameObject.SetActive(false);

            _currentWeaponIndex = (_currentWeaponIndex + 1) % PlayerView.Weapons.Length;

            PlayerView.Weapons[_currentWeaponIndex].gameObject.SetActive(true);
            AttackState.SetWeapon(PlayerView.Weapons[_currentWeaponIndex]);
        }
        #endregion
    }
}