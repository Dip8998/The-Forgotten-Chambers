using UnityEngine;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Particles
{
    public class ParticleController
    {
        public ParticleView View { get; private set; }

        private readonly ParticleScriptableObject _data;
        private readonly ParticlePool _pool;

        public ParticleController(ParticleView view, ParticleScriptableObject data, ParticlePool pool)
        {
            View = view;
            _data = data;
            _pool = pool;
            View.SetController(this);
        }

        public void Play(Vector3 position, Quaternion rotation)
        {
            View.transform.position = position;
            View.transform.rotation = rotation;
            View.ActivateAndPlay();
        }

        public void ReturnToPool()
        {
            View.gameObject.SetActive(false);
        }

        public float GetDataLifetime() => _data.lifetime;
    }
}