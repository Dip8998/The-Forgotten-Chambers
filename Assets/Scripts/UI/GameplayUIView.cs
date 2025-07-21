using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class GameplayUIView : MonoBehaviour
    {
        [SerializeField] private Slider playerHealthSlider;

        [SerializeField] private Image currentWeaponSprite;
        [SerializeField] private WeaponIconLibrary weaponIconLibrary;

        [SerializeField] private GameObject currentKeySprite;

        [SerializeField] private GameObject gameplayUIContainer;
        [SerializeField] private GameObject weaponUIContainer;

        public void SetMaxHealth(int health)
        {
            playerHealthSlider.maxValue = health;
            playerHealthSlider.value = health;
        }

        public void SetHealth(int health)
        {
            playerHealthSlider.value = health;
        }

        public void UpdateWeaponIcon(WeaponType weaponType)
        {
            Sprite newIcon = weaponIconLibrary.GetIcon(weaponType);
            if (newIcon != null)
            {
                currentWeaponSprite.sprite = newIcon;
            }
            else
            {
                Debug.LogWarning($"No icon found for weapon type: {weaponType}");
                currentWeaponSprite.sprite = null;
            }
        }

        public void KeyIcon(bool hasKey, bool active)
        {
            if (!hasKey)
            {
                currentKeySprite.SetActive(false);
            }
            else
            {
                currentKeySprite.SetActive(active);
            }
        }

        public void SetGameplayUIVisibility(bool isVisible)
        {
            gameplayUIContainer?.SetActive(isVisible);
        }

        public void SetWeaponUIVisibility(bool isVisible)
        {
            weaponUIContainer?.SetActive(isVisible);
        }
    }
}