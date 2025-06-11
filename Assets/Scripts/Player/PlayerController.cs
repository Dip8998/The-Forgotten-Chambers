using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerView playerView;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            InitializePlayerView();
        }

        private void InitializePlayerView()
        {
            playerView = Object.Instantiate(playerScriptableObject.playerPrefab);
            playerView.SetPlayerController(this);
        }
    }
}
