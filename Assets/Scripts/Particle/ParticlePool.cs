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
            if (parent == null)
            {
                this.parent = new GameObject($"ParticlePool_{data.particleType}").transform;
            }
            else
            {
                this.parent = parent;
            }

            for (int i = 0; i < count; i++)
            {
                CreateNewParticle();
            }
        }

        private ParticleController CreateNewParticle()
        {
            ParticleView view = Object.Instantiate(data.prefab, parent).GetComponent<ParticleView>();
            if (view == null)
            {
                Debug.LogError($"ParticlePool: Prefab for {data.particleType} does not have a ParticleView component on its root! Make sure the prefab itself is the ParticleView.", data.prefab);
                return null;
            }
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