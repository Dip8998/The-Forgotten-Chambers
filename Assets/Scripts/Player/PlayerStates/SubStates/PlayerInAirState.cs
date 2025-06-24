using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public class PlayerInAirState : PlayerState
    {
        private float _xInput;
        private bool _jumpInput;
        private bool _grabInput;
        private bool _coyoteTimeActive;
        private bool _wallJumpCoyoteTimeActive;
        private float _startWallJumpCoyoteTime;

        public PlayerInAirState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            if (Player.JumpState.AmountOfJumpsLeft == PlayerData.amountOfJumps)
            {
                Player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            CheckCoyoteTime();
            CheckWallCoyoteTime();

            _xInput = Player.InputHandler.MoveInput;
            _jumpInput = Player.InputHandler.JumpInput;
            _grabInput = Player.InputHandler.GrabInput;

            if (Player.InputHandler.AttackInput && !Player.CheckIsCeiling())
            {
                StateMachine.ChangeState(Player.AttackState);
            }
            else if (Player.CheckIsGround() && Player.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.LandState);
            }
            else if (_jumpInput)
            {
                if (Player.CheckIsWall() || Player.CheckIsWallBack() || _wallJumpCoyoteTimeActive)
                {
                    StopWallCoyoteTime();
                    Player.WallJumpState.DetermineWallJumpDirection(Player.CheckIsWall());
                    StateMachine.ChangeState(Player.WallJumpState);
                }
                else if (Player.JumpState.CanJump() && _coyoteTimeActive)
                {
                    _coyoteTimeActive = false;
                    StateMachine.ChangeState(Player.JumpState);
                }
                else if (Player.JumpState.CanJump())
                {
                    StateMachine.ChangeState(Player.JumpState);
                }
            }
            else if (Player.CheckIsWall() && _grabInput)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else if (Player.CheckIsWall() && _xInput == Player.FacingDirection && Player.CurrentVelocity.y <= 0)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
            else
            {
                Player.CheckIfShouldFlip(_xInput);
                Player.SetVelocityX(PlayerData.playerMovementSpeed * _xInput);

                Player.SetAnimatorFloat("yVelocity", Player.CurrentVelocity.y);
                Player.SetAnimatorFloat("xVelocity", Mathf.Abs(Player.CurrentVelocity.x));
            }
        }

        private void CheckCoyoteTime()
        {
            if (_coyoteTimeActive && Time.time > startTime + PlayerData.coyoteTime)
            {
                _coyoteTimeActive = false;
                if (Player.JumpState.AmountOfJumpsLeft == PlayerData.amountOfJumps)
                {
                    Player.JumpState.DecreaseAmountOfJumpsLeft();
                }
            }
        }

        private void CheckWallCoyoteTime()
        {
            if (_wallJumpCoyoteTimeActive && Time.time > _startWallJumpCoyoteTime + PlayerData.coyoteTime)
            {
                _wallJumpCoyoteTimeActive = false;
            }
        }

        public void StartCoyoteTime() => _coyoteTimeActive = true;

        public void StartWallCoyoteTime()
        {
            _wallJumpCoyoteTimeActive = true;
            _startWallJumpCoyoteTime = Time.time;
        }

        public void StopWallCoyoteTime() => _wallJumpCoyoteTimeActive = false;
    }
}