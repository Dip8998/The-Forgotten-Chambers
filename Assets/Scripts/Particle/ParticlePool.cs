using ForgottonChambers.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.Particles
{
    public class ParticlePool
    {
        private readonly ParticleScriptableObject data;
        private readonly Transform parent;
        private readonly List<ParticleController> pool = new();

        public ParticleType ParticleType => data.particleType;

        public ParticlePool(ParticleScriptableObject data, int count = 5, Transform parent = null)
        {
            this.data = data;
            this.parent = parent;

            for (int i = 0; i < count; i++)
            {
                CreateNewParticle();
            }
        }

        private ParticleController CreateNewParticle()
        {
            ParticleView view = Object.Instantiate(data.prefab, parent).GetComponent<ParticleView>();
            view.gameObject.SetActive(false);
            var controller = new ParticleController(view, data, this);
            pool.Add(controller);
            return controller;
        }

        public ParticleController GetParticle()
        {
            foreach (ParticleController p in pool)
            {
                if (!p.View.gameObject.activeInHierarchy)
                    return p;
            }

            return CreateNewParticle();
        }
    }
}
