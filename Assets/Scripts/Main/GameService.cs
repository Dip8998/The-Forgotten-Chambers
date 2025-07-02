using ForgottonChambers.Player;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Utilities;
using ForgottonChambers.Events;
using System.Collections.Generic;
using ForgottonChambers.Particles;
using ForgottonChambers.Bullets;

namespace ForgottonChambers.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public EventService EventService { get; private set; }
        public ParticleService ParticleService { get; private set; }
        public BulletService BulletService { get; private set; }
        public PlayerController PlayerController { get; private set; }

        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [Header("Particle Service Config")]
        [SerializeField] private List<ParticleScriptableObject> allParticleData;

        [Header("Bullet Service Config")]
        [SerializeField] private BulletView defaultBulletPrefab;
        [SerializeField] private BulletScriptableObject defaultBulletData;
        [SerializeField] private int defaultBulletPoolSize = 10;

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
            EventService = new EventService();
            ParticleService = new ParticleService(allParticleData);
            BulletService = new BulletService(defaultBulletPrefab, defaultBulletData, defaultBulletPoolSize);
        }

        public void RegisterPlayerController(PlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}