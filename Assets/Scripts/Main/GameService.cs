using ForgottonChambers.Player;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    public PlayerService playerService {  get; private set; }

    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    protected override void Awake()
    {
        base.Awake();
        playerService = new PlayerService(playerScriptableObject);
    }
}
