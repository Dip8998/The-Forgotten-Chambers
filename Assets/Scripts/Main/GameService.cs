using ForgottonChambers.Player;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Utilities;
using ForgottonChambers.Events;
using System.Collections.Generic;
using ForgottonChambers.Particles;

namespace ForgottonChambers.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        public PlayerService PlayerService { get; private set; }
        public EventService EventService { get; private set; }
        public ParticleService ParticleService { get; private set; }

        [SerializeField] private PlayerScriptableObject playerScriptableObject;
        [SerializeField] private List<ParticleScriptableObject> allParticles;



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
            ParticleService = new ParticleService(allParticles);
        }
    }
}