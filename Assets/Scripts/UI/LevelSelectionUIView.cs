using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ForgottonChambers.Main; 
using ForgottonChambers.Level; 

namespace ForgottonChambers.UI
{
    public class LevelSelectionUIView : MonoBehaviour
    {
        [SerializeField] private List<LevelButtonView> levelButtons;
        [SerializeField] private Button backButton;
        [SerializeField] private StartUIView StartUIVIew;

        private LevelSelectionUIController controller;

        private void OnEnable() 
        {
            backButton.onClick.AddListener(BackButton);
            UpdateLevelButtonStates(); 
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(BackButton);
        }

        public void SetController(LevelSelectionUIController controller)
        {
            this.controller = controller;

            for (int i = 0; i < levelButtons.Count; i++)
            {
                levelButtons[i].SetOwner(controller);
                levelButtons[i].SetLevelID(i + 1); 
            }
            UpdateLevelButtonStates();
        }

        private void BackButton()
        {
            SetViewInActive();
            StartUIVIew.gameObject.SetActive(true);
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
        }

        public void SetViewActive()
        {
            gameObject.SetActive(true);
            UpdateLevelButtonStates(); 
        }

        public void SetViewInActive()
        {
            gameObject.SetActive(false);
        }

        public void UpdateLevelButtonStates()
        {
            if (GameService.Instance == null || GameService.Instance.LevelService == null)
            {
                Debug.LogWarning("GameService.Instance or LevelService is null. Cannot update level button states.");
                return;
            }

            foreach (LevelButtonView button in levelButtons)
            {
                if (button == null)
                {
                    Debug.LogError("A null LevelButtonView found in the levelButtons list!");
                    continue;
                }

                int buttonLevelID = button.GetLevelID();
                LevelStatus status = GameService.Instance.LevelService.GetLevelStatus(buttonLevelID);
                Debug.Log($"Button for Level ID {buttonLevelID} is reporting status: {status}"); // Add this line
                button.UpdateButtonState(status);
            }
        }
    }
}