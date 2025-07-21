using UnityEngine;

namespace ForgottonChambers.Level
{
    public class LevelService
    {
        private LevelController levelController;

        public void Initialize(LevelView view)
        {
            levelController = new LevelController(view);
        }

        public void LoadNextLevel() => levelController?.OnPlayerReachedEnd();

        public void RestartLevel() => levelController?.RestartLevel();
    }
}
