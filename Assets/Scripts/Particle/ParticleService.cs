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
            foreach (var data in allParticleData)
            {
                if (!particlePools.ContainsKey(data.particleType))
                {
                    particlePools[data.particleType] = new ParticlePool(data);
                }
                else
                {
                    Debug.LogWarning($"Duplicate ParticleType: {data.particleType} in ParticleService");
                }
            }
        }

        public void PlayParticle(ParticleType type, Vector3 position, Quaternion rotation)
        {
            if (particlePools.TryGetValue(type, out ParticlePool pool))
            {
                pool.GetParticle().Play(position, rotation);
            }
            else
            {
                Debug.LogError($"No particle pool found for type: {type}");
            }
        }
    }
}
