using UnityEngine;

namespace ForgottonChambers.Particles
{
    public class ParticleView : MonoBehaviour
    {
        private ParticleController controller;

        public void SetController(ParticleController controller)
        {
            this.controller = controller;
        }

        private void OnEnable()
        {
        }

        public void DeactivateAfter(float time)
        {
            StartCoroutine(DeactivateRoutine(time));
        }

        private System.Collections.IEnumerator DeactivateRoutine(float time)
        {
            yield return new WaitForSeconds(time);
            controller.ReturnToPool();
        }
    }
}