using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState {  get; private set; }

        public void Initialize(EnemyState startingState)
        {
            CurrentState = startingState;
            CurrentState.OnStateEnter();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentState?.OnStateExit();
            CurrentState = newState;
            CurrentState?.OnStateEnter();
        }
    }
}