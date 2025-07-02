using ForgottonChambers.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.Particles
{
    public class ParticleService
    {
        private readonly Dictionary<ParticleType, ParticlePool> particlePools = new();

        public ParticleService(List<ParticleScriptableObject> allParticleData)
        {
            if (allParticleData == null || allParticleData.Count == 0)
            {
                Debug.LogWarning("ParticleService: No ParticleScriptableObjects provided for initialization.");
                return;
            }

            foreach (var data in allParticleData)
            {
                if (data == null)
                {
                    Debug.LogWarning("ParticleService: Found a null ParticleScriptableObject in the list.");
                    continue;
                }

                if (!particlePools.ContainsKey(data.particleType))
                {
                    particlePools[data.particleType] = new ParticlePool(data);
                }
                else
                {
                    Debug.LogWarning($"Duplicate ParticleType: {data.particleType} in ParticleService initialization list. Only the first instance will be used for pooling.");
                }
            }
        }

        public void PlayParticle(ParticleType type, Vector3 position, Quaternion rotation)
        {
            if (particlePools.TryGetValue(type, out ParticlePool pool))
            {
                ParticleController particle = pool.GetParticle();
                if (particle != null)
                {
                    particle.Play(position, rotation);
                }
            }
            else
            {
                Debug.LogError($"No particle pool found for type: {type}. Make sure you've added the corresponding ParticleScriptableObject to GameService's Particle Service Config list.");
            }
        }
    }
}