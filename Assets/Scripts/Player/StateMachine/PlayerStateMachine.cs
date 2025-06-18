using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerStateMachine 
    {
        public PlayerState currentState { get; private set; }

        public void InitializeState(PlayerState startingState)
        {
            currentState = startingState;
            currentState.OnStateEnter();
        }

        public void ChangeState(PlayerState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
    }
}