// LevelButtonView.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ForgottonChambers.Level;
using ForgottonChambers.Main;

namespace ForgottonChambers.UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private Image lockImage;

        private LevelSelectionUIController owner;
        private int levelId;
        private Button levelButton;

        private void Awake()
        {
            levelButton = GetComponent<Button>();

            if (levelButton == null)
            {
                enabled = false; 
                return;
            }

            levelButton.onClick.AddListener(OnLevelButtonClicked);
        }

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        private void OnLevelButtonClicked()
        {
            if (levelButton != null && levelButton.interactable)
            {
                owner.OnLevelSelected(levelId);
                GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
            }
        }

        public void SetLevelID(int levelId)
        {
            this.levelId = levelId;
            if (buttonText != null) 
            {
                buttonText.SetText("" + levelId);
            }
        }

        public int GetLevelID() => levelId;

        public void UpdateButtonState(LevelStatus status)
        {
            if (buttonText == null)
            {
                Debug.LogError($"LevelButtonView: CRITICAL! 'buttonText' is NULL in UpdateButtonState for GameObject {gameObject.name}. " +
                               "The reference was lost or never assigned. Cannot update text.");
                return; 
            }

            switch (status)
            {
                case LevelStatus.Locked:
                    if (levelButton != null) levelButton.interactable = false;
                    if (lockImage != null) lockImage.gameObject.SetActive(true);
                    break;
                case LevelStatus.Unlocked:
                    if (levelButton != null) levelButton.interactable = true;
                    if (lockImage != null) lockImage.gameObject.SetActive(false);
                    break;
                case LevelStatus.Completed:
                    if (levelButton != null) levelButton.interactable = true;
                    if (lockImage != null) lockImage.gameObject.SetActive(false);
                    break;
            }
        }
    }
}