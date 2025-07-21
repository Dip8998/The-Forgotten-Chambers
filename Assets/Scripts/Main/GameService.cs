using ForgottonChambers.Player;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Utilities;
using ForgottonChambers.Events;
using System.Collections.Generic;
using ForgottonChambers.Particles;
using ForgottonChambers.Bullets;
using ForgottonChambers.UI;
using ForgottonChambers.Level;
using ForgottonChambers.KeyandDoor;

namespace ForgottonChambers.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public EventService EventService { get; private set; }
        public ParticleService ParticleService { get; private set; }
        public BulletService BulletService { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public LevelService LevelService { get; private set; }
        public KeyAndDoorService KeyAndDoorService { get; private set; }

        [Header("UI")]
        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;

        [Header("PlayerSO")]
        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [Header("Particle Service Config")]
        [SerializeField] private List<ParticleScriptableObject> allParticleData;

        [Header("Bullet Service Config")]
        [SerializeField] private BulletView defaultBulletPrefab;
        [SerializeField] private BulletScriptableObject defaultBulletData;
        [SerializeField] private int defaultBulletPoolSize = 10;

        [Header("Level")]
        [SerializeField] private LevelView levelView;

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
                Debug.LogError("PlayerScriptableObject not assigned in GameService.");
                return;
            }

            EventService = new EventService();

            if (uiService == null)
            {
                Debug.LogError("UIService reference is missing in GameService.");
            }
            else
            {
                uiService.Initialize();
            }

            PlayerService = new PlayerService(playerScriptableObject);

            ParticleService = new ParticleService(allParticleData);
            BulletService = new BulletService(defaultBulletPrefab, defaultBulletData, defaultBulletPoolSize);
            KeyAndDoorService = new KeyAndDoorService();
            LevelService = new LevelService();
            LevelService.Initialize(levelView);
        }

        public void RegisterPlayerController(PlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}