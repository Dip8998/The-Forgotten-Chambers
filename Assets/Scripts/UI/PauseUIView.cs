using ForgottonChambers.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class PauseUIView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button mainMenuButton;

        private void Start()
        {
            restartButton.onClick.AddListener(Restart);
            mainMenuButton.onClick.AddListener(MainMenu);
            settingButton.onClick.AddListener(Settings);
        }

        public void Resume()
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Settings()
        {
            GameService.Instance.SettingUIView.gameObject.SetActive(true);
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            this.gameObject.SetActive(false);
        }

        private void Restart()
        {
            Time.timeScale = 1f;
            GameService.Instance.RestartCurrentLevel();
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            this.gameObject.SetActive(false);
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        private void MainMenu()
        {
            SceneManager.LoadScene(0);
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            Time.timeScale = 1f;
        }
    }
}