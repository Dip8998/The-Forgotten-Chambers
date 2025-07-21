using UnityEngine;

namespace ForgottonChambers.Level
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelView : MonoBehaviour
    {
        private LevelController controller;

        public void Initialize(LevelController controller)
        {
            this.controller = controller;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.PlayerView playerView))
            {
                controller?.OnPlayerReachedEnd();
            }
        }
    }
}