using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Events;
using UnityEditor.MPE;
using ForgottonChambers.Main;

namespace ForgottonChambers.UI
{
    public class GameplayUIController
    {
        private GameplayUIView gameplayView;

        public GameplayUIController(GameplayUIView gameplayView)
        {
            this.gameplayView = gameplayView;

            GameService.Instance.EventService.OnPlayerHealthChangedEvent.AddListener(SetPlayerHealth);
            GameService.Instance.EventService.OnPlayerMaxHealthSetEvent.AddListener(SetPlayerMaxHealth);
            GameService.Instance.EventService.OnWeaponPickedUpEvent.AddListener(SetWeaponIcon);
            GameService.Instance.EventService.OnKeyCollectedEvent.AddListener(() => SetKeyIcon(true, true));
            GameService.Instance.EventService.OnDoorOpenedEvent.AddListener(() => SetKeyIcon(false, false));
            GameService.Instance.EventService.OnGameplayUIVisibilityChanged.AddListener(gameplayView.SetGameplayUIVisibility);
            GameService.Instance.EventService.OnWeaponUIVisibilityChanged.AddListener(gameplayView.SetWeaponUIVisibility);
        }

        ~GameplayUIController()
        {
            GameService.Instance.EventService.OnPlayerHealthChangedEvent.RemoveListener(SetPlayerHealth);
            GameService.Instance.EventService.OnPlayerMaxHealthSetEvent.RemoveListener(SetPlayerMaxHealth);
            GameService.Instance.EventService.OnWeaponPickedUpEvent.RemoveListener(SetWeaponIcon);
            GameService.Instance.EventService.OnKeyCollectedEvent.RemoveListener(() => SetKeyIcon(true, true));
            GameService.Instance.EventService.OnDoorOpenedEvent.RemoveListener(() => SetKeyIcon(false, false));
            GameService.Instance.EventService.OnGameplayUIVisibilityChanged.RemoveListener(gameplayView.SetGameplayUIVisibility);
            GameService.Instance.EventService.OnWeaponUIVisibilityChanged.RemoveListener(gameplayView.SetWeaponUIVisibility);
        }

        public void SetPlayerHealth(int health) => gameplayView.SetHealth(health);
        
        public void SetPlayerMaxHealth(int health) => gameplayView.SetMaxHealth(health);
        
        public void SetWeaponIcon(WeaponType type) => gameplayView.UpdateWeaponIcon(type);
        
        public void SetKeyIcon(bool hasKey, bool active) => gameplayView.KeyIcon(hasKey, active);
    }
}