using ForgottonChambers.Main;
using UnityEngine;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class StartUIView : MonoBehaviour
    {
        private LevelSelectionUIController owner;

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        public void ShowLevelSelection()
        {
            owner.Show();
            GameService.Instance.SoundService.Play(Sound.Sounds.BUTTONCLICK);
        }
    }
}
