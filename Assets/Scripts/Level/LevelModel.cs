using UnityEngine;
using UnityEngine.SceneManagement;

namespace ForgottonChambers.Level
{
    public class LevelModel
    {
        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public int TotalScenes => SceneManager.sceneCountInBuildSettings;

        public bool HasNextLevel() => CurrentSceneIndex + 1 < TotalScenes;
        public int GetNextLevelIndex() => HasNextLevel() ? CurrentSceneIndex + 1 : -1;
    }
}
