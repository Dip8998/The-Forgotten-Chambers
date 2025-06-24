using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Weapons;
using System.Collections.Generic;
using ForgottonChambers.Player.Interfaces;
using ForgottonChambers.Inputs;

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
        public InputHandler InputHandler { get; private set; }
        public PlayerView PlayerView { get; private set; } 
        public BoxCollider2D MovementCollider { get; private set; } 

        private IPlayerMover _playerMover;
        private Vector2 _workSpace;
        public Vector2 CurrentVelocity => _playerMover.GetCurrentVelocity();
        #endregion

        #region Weapon Management
        private Dictionary<WeaponType, WeaponController> _weaponControllers;
        private WeaponType _currentWeaponType;
        private List<WeaponType> _collectedWeapons = new();
        private int _currentWeaponIndex = 0;
        #endregion

        #region Other Variables
        public int FacingDirection { get; private set; } = 1;
        #endregion

        #region Player Callbacks
        public PlayerController(PlayerScriptableObject playerConfig)
        {
            this.playerConfig = playerConfig;
            StateMachine = new PlayerStateMachine();
            InputHandler = new InputHandler();
            _workSpace = Vector2.zero;

            InitializePlayerView();
            InitializePlayerStates();
            InitializeWeaponControllers();
        }

        public void SetupPlayer()
        {
            MovementCollider = PlayerView.GetPlayerCollider();
            _playerMover = PlayerView; 

            PlayerView.OnAnimationFinishedEvent += AnimationFinishedTrigger;

            _currentWeaponType = WeaponType.Punch;
            WeaponController defaultWeaponController = _weaponControllers[_currentWeaponType];
            AttackState.SetWeapon(defaultWeaponController.WeaponView);
            PlayerView.SetWeaponGameObjectActive(_currentWeaponType, true);

            _collectedWeapons = new List<WeaponType> { WeaponType.Punch };
            _currentWeaponIndex = 0;

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
                Debug.LogError("Player Prefab is null in PlayerConfig!");
                return;
            }
            PlayerView = Object.Instantiate(playerConfig.playerPrefab);
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

        private void InitializeWeaponControllers()
        {
            _weaponControllers = new Dictionary<WeaponType, WeaponController>();
            foreach (WeaponView weaponView in PlayerView.Weapons)
            {
                _weaponControllers.Add(weaponView.WeaponController.WeaponData.weaponType, weaponView.WeaponController);
            }
        }
        #endregion

        #region Movement and Velocity Application
        public void SetVelocityZero() => _playerMover.SetLinearVelocity(Vector2.zero);
        public void SetVelocityX(float velocity) => _playerMover.SetVelocityX(velocity);
        public void SetVelocityY(float velocity) => _playerMover.SetVelocityY(velocity);
        public void SetVelocity(float speed, Vector2 angle, int dir)
        {
            angle.Normalize();
            _workSpace.Set(angle.x * speed * dir, angle.y * speed);
            _playerMover.SetLinearVelocity(_workSpace);
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
            PlayerView.SetRotationY(FacingDirection == 1 ? 0f : 180f);
        }

        public bool CheckIsGround() => PlayerView.IsGrounded();
        public bool CheckIsWall() => PlayerView.IsTouchingWall();
        public bool CheckIsWallBack() => PlayerView.IsTouchingWallBack();
        public bool CheckIsCeiling() => PlayerView.IsCeiling();
        public bool HasBoxAttached() => PlayerView.HasBoxAttached();

        public void SetColliderSize(Vector2 newSize, Vector2 newOffset)
        {
            PlayerView.GetPlayerCollider().size = newSize;
            PlayerView.GetPlayerCollider().offset = newOffset;
        }

        public void SetAnimatorBool(string paramName, bool value) => PlayerView.SetAnimatorBool(paramName, value);
        public void SetAnimatorFloat(string paramName, float value) => PlayerView.SetAnimatorFloat(paramName, value);
        public void SetAnimatorTrigger(string paramName) => PlayerView.SetAnimatorTrigger(paramName);


        private void AnimationFinishedTrigger() => StateMachine.currentState.AnimationFinishTrigger();

        public void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();
        #endregion

        #region Weapon switching Functions

        public void AddWeaponToInventory(WeaponType newWeaponType)
        {
            if (_collectedWeapons.Contains(newWeaponType)) return;

            _collectedWeapons.Add(newWeaponType);
            Debug.Log($"Collected: {newWeaponType}");

            if (_collectedWeapons.Count == 1 && newWeaponType != WeaponType.Punch)
            {
                SwitchWeaponTo(newWeaponType);
            }
        }

        private void SwitchWeapon()
        {
            if (_collectedWeapons.Count <= 1) return;
            if (StateMachine.currentState == AttackState && PlayerView.GetAnimatorBool("attack")) return;

            _currentWeaponIndex = (_currentWeaponIndex + 1) % _collectedWeapons.Count;
            var nextWeapon = _collectedWeapons[_currentWeaponIndex];
            SwitchWeaponTo(nextWeapon);
        }

        private void SwitchWeaponTo(WeaponType type)
        {
            if (!_weaponControllers.ContainsKey(type))
            {
                Debug.LogWarning($"Weapon {type} not found.");
                return;
            }

            PlayerView.SetWeaponGameObjectActive(_currentWeaponType, false);
            PlayerView.SetWeaponGameObjectActive(type, true);

            AttackState.SetWeapon(_weaponControllers[type].WeaponView);
            _currentWeaponType = type;
        }
        #endregion
    }
}