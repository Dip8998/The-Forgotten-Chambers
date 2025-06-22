using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Player
{
    public abstract class PlayerState
    {
        protected PlayerController Player;
        protected PlayerStateMachine StateMachine;
        protected PlayerScriptableObject PlayerData;
        protected bool isAnimationFinished;
        protected bool isExitingState;

        protected float startTime;

        private readonly string _animBoolName;

        public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
        {
            Player = player;
            StateMachine = stateMachine;
            PlayerData = playerData;
            _animBoolName = animBoolName;
        }

        public virtual void OnStateEnter()
        {
            startTime = Time.time;
            Player.PlayerView.PlayerAnimator.SetBool(_animBoolName, true);
            isAnimationFinished = false;
            isExitingState = false;
        }

        public virtual void OnStateExit()
        {
            Player.PlayerView.PlayerAnimator.SetBool(_animBoolName, false);
            isExitingState = true;
        }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}