using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ForgottonChambers.UI
{
    public class ScoreUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI[] highScoreText;
        [SerializeField] private GameObject scoreUIContainer;

        private Dictionary<int, float> levelScores = new Dictionary<int, float>();
        private float highScore;
        private int currentLevelID;

        private const string HighScoreKey = "GlobalHighScore";

        private void Awake()
        {
            LoadHighScore();
        }

        public void InitializeLevel(int levelID)
        {
            currentLevelID = levelID;

            levelScores[currentLevelID] = 0;

            UpdateScoreDisplay();
        }

        public void AddScore(float amount)
        {
            if (!levelScores.ContainsKey(currentLevelID))
                levelScores[currentLevelID] = 0;

            levelScores[currentLevelID] += amount;

            float totalScore = GetTotalScore();
            if (totalScore > highScore)
            {
                highScore = totalScore;
                SaveHighScore();
            }

            UpdateScoreDisplay();
        }
        public void ResetCurrentLevelScore()
        {
            levelScores[currentLevelID] = 0;
            UpdateScoreDisplay();
        }

        public void ResetAll()
        {
            levelScores.Clear();
            highScore = 0;
            PlayerPrefs.DeleteKey(HighScoreKey);
            PlayerPrefs.Save();
            UpdateScoreDisplay();
        }

        public void SetScoreUIVisibility(bool isVisible)
        {
            scoreUIContainer?.SetActive(isVisible);
        }

        private void UpdateScoreDisplay()
        {
            float currentLevelScore = levelScores.ContainsKey(currentLevelID) ? levelScores[currentLevelID] : 0f;
            scoreText.text = $"Score: {currentLevelScore:F0}";
            highScoreText[0].text = $"High Score: {highScore}";
            highScoreText[1].text = $"High Score: {highScore}";
            highScoreText[2].text = $"High Score: {highScore}";
        }


        private float GetTotalScore()
        {
            return levelScores.Values.Sum();
        }

        private void SaveHighScore()
        {
            PlayerPrefs.SetFloat(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        private void LoadHighScore()
        {
            highScore = PlayerPrefs.GetFloat(HighScoreKey, 0);
            UpdateScoreDisplay() ;
        }
    }
}
