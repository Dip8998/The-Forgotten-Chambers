// In PlayerService.cs
using ForgottonChambers.Main;
using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;

public class PlayerService
{
    private PlayerScriptableObject _playerConfig;
    private PlayerController _playerController;

    public PlayerService(PlayerScriptableObject playerConfig)
    {
        this._playerConfig = playerConfig;
        SubscribeToEvents(); 
    }

    private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnPlayer);
    private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);

    public void SpawnPlayer(int levelId)
    {
        if (_playerController == null)
        {
            _playerController = new PlayerController(_playerConfig);
        }

        _playerController.SetupPlayer();

        _playerController.SetInitialWeaponsForLevel(levelId);
        UnsubscribeToEvents(); 
    }

    public PlayerController GetPlayerController() => _playerController;

    public void TriggerRespawn()
    {
        GameService.Instance.StartCoroutine(_playerController.Respawn());
    }
}