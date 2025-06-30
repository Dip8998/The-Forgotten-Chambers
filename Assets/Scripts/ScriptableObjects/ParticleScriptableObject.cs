using ForgottonChambers.Particles;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ParticleData", menuName = "ScriptableObjects/ParticleData")]
    public class ParticleScriptableObject : ScriptableObject
    {
        public ParticleType particleType;
        public GameObject prefab;
        public float lifetime;
    }
}
