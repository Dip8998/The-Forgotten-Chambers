using UnityEngine.SceneManagement;

namespace ForgottonChambers.Level
{
    public class LevelController
    {
        private LevelModel _model;
        private LevelView _view;

        public LevelController(LevelView view)
        {
            _model = new LevelModel();
            _view = view;
            _view.Initialize(this);
        }

        public void OnPlayerReachedEnd()
        {
            if (_model.HasNextLevel())
            {
                SceneManager.LoadScene(_model.GetNextLevelIndex());
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(_model.CurrentSceneIndex);
        }
    }
}