using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Player
{
    public class PlayerService
    {
        private readonly PlayerScriptableObject _playerConfig;
        private PlayerController _playerController;

        public PlayerService(PlayerScriptableObject playerConfig)
        {
            _playerConfig = playerConfig;
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            if (_playerConfig == null)
            {
                return;
            }
            if (_playerConfig.playerPrefab == null)
            {
                return;
            }

            _playerController = new PlayerController(_playerConfig);
        }

        public PlayerController GetPlayerController()
        {
            if (_playerController == null)
            {
            }
            return _playerController;
        }
    }
}
