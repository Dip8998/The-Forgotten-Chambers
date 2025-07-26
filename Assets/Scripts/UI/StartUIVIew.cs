using UnityEngine;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class StartUIVIew : MonoBehaviour
    {
        private LevelSelectionUIController owner;

        public void SetOwner(LevelSelectionUIController owner) => this.owner = owner;

        public void ShowLevelSelection(int levelCount) => owner.Show(levelCount);
    }
}
