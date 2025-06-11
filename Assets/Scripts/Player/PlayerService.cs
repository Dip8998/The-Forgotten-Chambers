using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerService 
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerController playerController;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            playerController = new PlayerController(playerScriptableObject);
        }
    }
}
