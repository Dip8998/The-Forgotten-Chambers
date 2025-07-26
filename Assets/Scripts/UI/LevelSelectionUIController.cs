using ForgottonChambers.Main;
using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.UI
{
    public class LevelSelectionUIController
    {
        private LevelSelectionUIView levelSelectionView;
        private StartUIVIew startUIVIew;
        private LevelButtonView levelButtonPrefab;
        private List<LevelButtonView> levelButtons;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionView, LevelButtonView levelButtonView, StartUIVIew startUIVIew)
        {
            this.levelSelectionView = levelSelectionView;
            this.levelButtonPrefab = levelButtonView;
            this.startUIVIew = startUIVIew;
            startUIVIew.SetOwner(this);
            levelSelectionView.SetController(this);
            InitializeController();
        }

        private void InitializeController()
        {
            levelButtons = new List<LevelButtonView>();
            Hide();
        }

        public void Show(int levelCount)
        {
            levelSelectionView.SetViewActive();
            CreateLevelButtons(levelCount);
        }

        public void Hide()
        {
            ResetLevelButtons();
            levelSelectionView.SetViewInActive();
        }

        private void ResetLevelButtons()
        {
            levelButtons.ForEach(button => Object.Destroy(button.gameObject));
            levelButtons.Clear();
        }

        public void CreateLevelButtons(int levelCount)
        {
            for (int i = 1; i <= levelCount; i++)
            {
                var newButton = levelSelectionView.AddButton(levelButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetLevelID(i);
            }
        }

        public void OnLevelSelected(int levelId)
        {
            GameService.Instance.EventService.OnLevelSelected.InvokeEvent(levelId);
            Hide();
        }
    }
}
