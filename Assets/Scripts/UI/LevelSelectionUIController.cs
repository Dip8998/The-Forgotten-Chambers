using ForgottonChambers.Main;
using UnityEngine;

namespace ForgottonChambers.UI
{
    public class LevelSelectionUIController
    {
        private LevelSelectionUIView levelSelectionView;
        private StartUIView startUIVIew;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionView, StartUIView startUIVIew)
        {
            this.levelSelectionView = levelSelectionView;
            this.startUIVIew = startUIVIew;
            startUIVIew.SetOwner(this);
            levelSelectionView.SetController(this);
            InitializeController();
        }

        private void InitializeController()
        {
            Hide();
        }

        public void Show()
        {
            levelSelectionView.SetViewActive();
        }

        public void Hide()
        {
            levelSelectionView.SetViewInActive();
        }

        public void OnLevelSelected(int levelId)
        {
            GameService.Instance.LevelService.LoadLevel(levelId); 
            GameService.Instance.EventService.OnLevelSelected.InvokeEvent(levelId); 
            Hide();
        }
    }
}
