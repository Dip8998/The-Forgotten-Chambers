using TMPro;
using UnityEngine;

namespace ForgottonChambers.UI
{
    public class ScoreUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject scoreUIContainer;

        private float currentScore;

        private void Awake()
        {
            currentScore = 0;
            UpdateScoreDisplay();
            scoreUIContainer.SetActive(false);
        }

        public void AddScore(float amount)
        {
            currentScore += amount;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            scoreText.text = $"Score: {currentScore:F0}";
        }

        public void SetScoreUIVisibility(bool isVisible)
        {
            scoreUIContainer?.SetActive(isVisible);
        }
    }
}