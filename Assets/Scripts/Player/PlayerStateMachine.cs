using ForgottonChambers.StateMachine;
using System.Collections.Generic;

namespace ForgottonChambers.Player
{
    public class PlayerStateMachine
    {
        private IState currentState;
        private Dictionary<PlayerState, IState> states = new Dictionary<PlayerState, IState>();

        public PlayerStateMachine(PlayerController playerController)
        {
            CreateState(playerController);
        }

        private void CreateState(PlayerController playerController)
        {
            states.Add(PlayerState.Idle, new IdleState(playerController, this));
            states.Add(PlayerState.Run, new RunState(playerController, this));
            states.Add(PlayerState.Jump, new JumpState(playerController, this));
            states.Add(PlayerState.DoubleJump, new DoubleJumpState(playerController, this));
            states.Add(PlayerState.Punch, new PunchState(playerController, this));
            states.Add(PlayerState.Crouching, new CrouchingState(playerController, this));
            states.Add(PlayerState.CrouchWalk, new CrouchWalkState(playerController, this));
        }

        public void Initialize(PlayerState startingPlayerState)
        {
            if(states.TryGetValue(startingPlayerState, out IState startingState))
            {
                currentState = startingState;
                currentState?.OnStateEnter();
            }
        }

        public void ChangeState(PlayerState newPlayerState)
        {
            if(states.TryGetValue(newPlayerState, out IState newState))
            {
                currentState?.OnStateExit();
                currentState = newState;
                currentState?.OnStateEnter();
            }
        }

        public void UpdateState() => currentState?.UpdateState();

        public void FixedUpdateState() => currentState?.FixedUpdateState();
    }
}
