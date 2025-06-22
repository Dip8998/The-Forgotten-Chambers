using ForgottonChambers.Player;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Utilities;

namespace ForgottonChambers.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }

        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        protected override void Awake()
        {
            base.Awake();

            if (Instance == this)
            {
                InitializeServices();
            }
        }

        private void InitializeServices()
        {
            if (playerScriptableObject == null)
            {
                return;
            }
            PlayerService = new PlayerService(playerScriptableObject);
        }
    }
}