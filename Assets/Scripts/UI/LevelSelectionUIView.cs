using UnityEngine;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class LevelSelectionUIView : MonoBehaviour
    {
        private LevelSelectionUIController controller;
        [SerializeField] private Transform levelButtonContainer;

        public void SetController( LevelSelectionUIController controller)
        {
            this.controller = controller;
        }

        public void SetViewActive() => this.gameObject.SetActive(true);

        public void SetViewInActive() => this.gameObject.SetActive(false);

        public LevelButtonView AddButton(LevelButtonView levelButtonPrefab) => Instantiate(levelButtonPrefab, levelButtonContainer);
    }
}