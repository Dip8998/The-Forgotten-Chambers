using ForgottonChambers.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class GameOverUIView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            mainMenuButton.onClick.AddListener(MainMenu);
        }

        private void RestartGame()
        {
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            Time.timeScale = 1f;
            GameService.Instance.RestartCurrentLevel();
            this.gameObject.SetActive(false);
        }

        private void MainMenu()
        {
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            SceneManager.LoadScene(0);
        }
    }
}