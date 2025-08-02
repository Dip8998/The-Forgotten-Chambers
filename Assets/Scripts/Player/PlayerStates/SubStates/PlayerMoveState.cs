using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Main;
using System.Collections;

namespace ForgottonChambers.Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        private bool isPlayingFootsteps = false;
        public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine, PlayerScriptableObject playerData, string animBoolName)
            : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isExitingState) return;

            Player.CheckIfShouldFlip(moveInput);
            Player.SetVelocityX(PlayerData.playerMovementSpeed * moveInput);
            if (!isPlayingFootsteps)
            {
                GameService.Instance.StartCoroutine(PlayFootstepSound());
            }

            if (moveInput == 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (verticalInput == -1)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
        }

        private IEnumerator PlayFootstepSound()
        {
            isPlayingFootsteps = true;
            if(Player.PlayerView.RB != null)
            {
                while ((Mathf.Abs(Player.PlayerView.RB.linearVelocity.x) > 0.1f) && Player.CheckIsGround())
                {
                    GameService.Instance.SoundService.Play(Sound.Sounds.PLAYERMOVE);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            isPlayingFootsteps = false;
        }

    }
}