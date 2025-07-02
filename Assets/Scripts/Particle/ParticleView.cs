using UnityEngine;
using System.Collections;

namespace ForgottonChambers.Particles
{
    public class ParticleView : MonoBehaviour
    {
        private ParticleController controller;
        private ParticleSystem particleSystemComponent;

        private void Awake()
        {
            particleSystemComponent = GetComponent<ParticleSystem>();
        }

        public void SetController(ParticleController controller)
        {
            this.controller = controller;
        }

        public void ActivateAndPlay()
        {
            gameObject.SetActive(true);

            if (particleSystemComponent != null)
            {
                particleSystemComponent.Play();
                StartCoroutine(DeactivateAfterParticleSystemDuration(particleSystemComponent.main.duration));
            }
            else if (controller != null)
            {
                StartCoroutine(DeactivateRoutine(controller.GetDataLifetime()));
            }
            else
            {
                Debug.LogError("ParticleView: Cannot play or deactivate without a ParticleSystem or a valid controller and data.", this);
            }
        }

        private IEnumerator DeactivateAfterParticleSystemDuration(float duration)
        {
            yield return new WaitForSeconds(duration);

            if (controller != null)
            {
                controller.ReturnToPool();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private IEnumerator DeactivateRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            if (controller != null)
            {
                controller.ReturnToPool();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}