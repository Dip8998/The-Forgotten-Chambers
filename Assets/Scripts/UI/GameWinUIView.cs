using UnityEngine;
using UnityEngine.UI;
using ForgottonChambers.Main;
using UnityEngine.SceneManagement;

namespace ForgottonChambers.UI
{
    public class GameWinUIView : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button mainMenuButton;

        private int nextLevelIdToLoad;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartLevel);
            if (nextLevelButton != null )
            {
                nextLevelButton.onClick.AddListener(NextLevel);
            }
            mainMenuButton.onClick.AddListener(MainMenu);
        }

        public void SetNextLevelId(int id)
        {
            nextLevelIdToLoad = id;
        }

        private void RestartLevel()
        {
            Time.timeScale = 1.0f;
            GameService.Instance.RestartCurrentLevel();
            this.gameObject.SetActive(false);
        }

        private void NextLevel()
        {
            Time.timeScale = 1.0f;
            GameService.Instance.EventService.OnLevelSelected.InvokeEvent(nextLevelIdToLoad);
            this.gameObject.SetActive(false);
        }

        private void MainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}