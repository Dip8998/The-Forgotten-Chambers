using ForgottonChambers.Player;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerState
    {
        protected PlayerController player;
        protected PlayerStateMachine stateMachine;
        protected PlayerScriptableObject playerData;
        protected bool isAnimationFinished;
        protected bool isExitingState;

        protected float startTime;

        private string animBoolName;

        public PlayerState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerDate, string animBoolName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerDate;
            this.animBoolName = animBoolName;
        }

        public virtual void OnStateEnter()
        {
            startTime = Time.time;
            player.playerView.playerAnimator.SetBool(animBoolName, true);
            isAnimationFinished = false; 
            isExitingState = false;
        }

        public virtual void OnStateExit()
        {
            player.playerView.playerAnimator.SetBool(animBoolName, false);
            isExitingState = true;
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}
    
