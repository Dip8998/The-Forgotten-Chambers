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
using UnityEngine.UI;
using ForgottonChambers.Level;
using ForgottonChambers.Sound;
using ForgottonChambers.Inputs;

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
        public SoundService SoundService { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public GameWinUIView LevelWinUIView => levelWinUIView;
        public GameOverUIView GameOverUIView => gameOverUIView;
        public GameWinUIView GameWinUIView => gameWinUIView;
        public PauseUIView PauseUIView => pauseUIView;
        public SettingUIView SettingUIView => settingUIView;
        private PauseService pauseService;

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

        [SerializeField] private GameWinUIView levelWinUIView;
        [SerializeField] private GameWinUIView gameWinUIView;
        [SerializeField] private GameOverUIView gameOverUIView;
        [SerializeField] private PauseUIView pauseUIView;
        [SerializeField] private SettingUIView settingUIView;

        [SerializeField] private SoundService soundService;

    
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

            if (UIService != null && UIService.LevelSelectionUIView != null)
            {
                UIService.LevelSelectionUIView.UpdateLevelButtonStates();
            }
        }

        private void LevelSelection() => uiService.InvokStart(levelScriptableObjects.Count);

        private void Update()
        {
            pauseService.UpdatePause();
        }

        private void InitializeServices()
        {
            EventService = new EventService();
            LevelService = new LevelService(levelScriptableObjects); 
            uiService.Initialize(); 
            PlayerService = new PlayerService(playerScriptableObject);
            InputHandler = new InputHandler();
            pauseService = new PauseService();
            ParticleService = new ParticleService(allParticleData);
            BulletService = new BulletService(defaultBulletPrefab, defaultBulletData, defaultBulletPoolSize);
            KeyAndDoorService = new KeyAndDoorService();

            SoundService = soundService;

            EventService.OnLevelSelected.AddListener(SetCurrentLevelID);
            EventService.OnLevelSelected.AddListener(LevelService.LoadLevel);
        }

        private void SetCurrentLevelID(int levelID)
        {
            CurrentLevelID = levelID;
        }

        public void RestartCurrentLevel()
        {
            LevelService.LoadLevel(CurrentLevelID);
        }

        public void PauseGame()
        {
            pauseUIView.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Debug.Log("Attempting to resume game...");
            pauseUIView.Resume();
        }
    }
}