using System.Collections.Generic;
using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Main;

namespace ForgottonChambers.Level
{
    public class LevelService
    {
        private List<LevelScriptableObject> levelScriptableObjects;
        private GameObject currentLevelInstance;

        public GameObject CurrentLevelParent => currentLevelInstance;

        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
            InitializeLevelStatuses();
        }

        private void InitializeLevelStatuses()
        {
            int firstLevelID = levelScriptableObjects[0].ID;

            if (!PlayerPrefs.HasKey(GetLevelKey(firstLevelID)))
            {
                SetLevelStatus(firstLevelID, LevelStatus.Unlocked);
            }
        }

        public void LoadLevel(int levelID)
        {
            LevelStatus currentLevelStatus = GetLevelStatus(levelID);

            if (currentLevelStatus == LevelStatus.Locked)
            {
                Debug.Log($"Level {levelID} is locked and cannot be loaded.");
                return;
            }

            LevelScriptableObject levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == levelID);

            if (levelData == null)
            {
                Debug.LogError($"No Level found with ID: {levelID}");
                return;
            }

            if (currentLevelInstance != null)
            {
                Object.Destroy(currentLevelInstance);
                Debug.Log("Previous level destroyed.");
            }

            currentLevelInstance = Object.Instantiate(levelData.LevelPrefab);
            Debug.Log($"Level {levelID} loaded.");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameService.Instance.UIService.InitializeScoreUI(levelID);
            GameService.Instance.PlayerService.SpawnPlayer(levelID);
        }

        public LevelStatus GetLevelStatus(int levelID)
        {
            string levelKey = GetLevelKey(levelID);
            LevelStatus status = (LevelStatus)PlayerPrefs.GetInt(levelKey, (int)LevelStatus.Locked);
            Debug.Log($"LevelService.GetLevelStatus called for ID {levelID}, retrieved status: {status}"); // Add this line
            return status;
        }

        public void SetLevelStatus(int levelID, LevelStatus status)
        {
            string levelKey = GetLevelKey(levelID);
            PlayerPrefs.SetInt(levelKey, (int)status);
            PlayerPrefs.Save();
            Debug.Log($"Level {levelID} status set to: {status}");
        }

        public void MarkLevelComplete(int completedLevelID)
        {
            SetLevelStatus(completedLevelID, LevelStatus.Completed);
            Debug.Log($"Marked Level {completedLevelID} as Completed.");

            int completedLevelIndex = levelScriptableObjects.FindIndex(levelSO => levelSO.ID == completedLevelID);

            if (completedLevelIndex != -1 && completedLevelIndex + 1 < levelScriptableObjects.Count)
            {
                int nextLevelID = levelScriptableObjects[completedLevelIndex + 1].ID;
                LevelStatus nextLevelStatus = GetLevelStatus(nextLevelID);

                if (nextLevelStatus == LevelStatus.Locked)
                {
                    SetLevelStatus(nextLevelID, LevelStatus.Unlocked);
                    Debug.Log($"Unlocked Level {nextLevelID}.");
                }
            }
        }

        private string GetLevelKey(int levelID)
        {
            return "Level_" + levelID + "_Status";
        }
    }
}