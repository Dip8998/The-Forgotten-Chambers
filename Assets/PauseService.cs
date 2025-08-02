using UnityEngine;
using ForgottonChambers.Main;

public class PauseService 
{
    public void UpdatePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameService.Instance.LevelWinUIView.gameObject.activeInHierarchy ||
                GameService.Instance.GameOverUIView.gameObject.activeInHierarchy ||
                GameService.Instance.GameWinUIView.gameObject.activeInHierarchy ||
                GameService.Instance.UIService.StartUIView.gameObject.activeInHierarchy ||
                GameService.Instance.UIService.LevelSelectionUIView.gameObject.activeInHierarchy ||
                GameService.Instance.SettingUIView.gameObject.activeInHierarchy)
            {
                return;
            }
            if (Time.timeScale == 0f)
            {
                GameService.Instance.ResumeGame();
            }
            else
            {
                GameService.Instance.PauseGame();

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}