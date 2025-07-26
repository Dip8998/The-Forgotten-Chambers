using ForgottonChambers.Player;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Utilities;
using ForgottonChambers.Events;
using System.Collections.Generic;
using ForgottonChambers.Particles;
using ForgottonChambers.Bullets;
using ForgottonChambers.UI;
using ForgottonChambers.KeyandDoor;
using StatePattern.Level;
using UnityEngine.UI;

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

        [SerializeField] private List<LevelScriptableObject> levelScriptableObjects;
        [SerializeField] private Button startButton;

        [Header("Bullet Service Config")]
        [SerializeField] private BulletView defaultBulletPrefab;
        [SerializeField] private BulletScriptableObject defaultBulletData;
        [SerializeField] private int defaultBulletPoolSize = 10;

        public int CurrentLevelID { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            if (Instance == this)
            {
                InitializeServices();
            }
        }

        private void Start()
        {
            UIService.Show();
            startButton.onClick.AddListener(LevelSelection);
        }

        private void LevelSelection() => uiService.InvokStart(levelScriptableObjects.Count);


        private void InitializeServices()
        {
            EventService = new EventService();
            uiService.Initialize();
            LevelService = new LevelService(levelScriptableObjects);
            PlayerService = new PlayerService(playerScriptableObject);
            ParticleService = new ParticleService(allParticleData);
            BulletService = new BulletService(defaultBulletPrefab, defaultBulletData, defaultBulletPoolSize);
            KeyAndDoorService = new KeyAndDoorService();

            EventService.OnLevelSelected.AddListener(SetCurrentLevelID);
        }

        private void SetCurrentLevelID(int levelID)
        {
            CurrentLevelID = levelID;
            Debug.Log($"Current Level ID set to: {CurrentLevelID}");
        }
    }
}