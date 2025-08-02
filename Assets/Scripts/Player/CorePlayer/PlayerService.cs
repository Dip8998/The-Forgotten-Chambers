using ForgottonChambers.Main;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;

public class PlayerService
{
    private PlayerScriptableObject _playerConfig;
    private PlayerController _playerController;

    public PlayerService(PlayerScriptableObject playerConfig)
    {
        this._playerConfig = playerConfig;
    }

    public void SpawnPlayer(int levelId)
    {
        if (_playerController != null && _playerController.PlayerView != null)
        {
            Object.Destroy(_playerController.PlayerView.gameObject);
            _playerController = null; 
            Debug.Log("Old player destroyed.");
        }


        _playerController = new PlayerController(_playerConfig);
        _playerController.SetupPlayer();
        _playerController.SetInitialWeaponsForLevel(levelId);
    }


    public PlayerController GetPlayerController() => _playerController;

    public void TriggerRespawn()
    {
        if (_playerController == null || _playerController.PlayerView == null) return;

        GameService.Instance.StartCoroutine(_playerController.Respawn());
        GameService.Instance.UIService.ResetScore();
    }

}