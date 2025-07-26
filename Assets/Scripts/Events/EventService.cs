using ForgottonChambers.ScriptableObjects;
using UnityEditor.MPE;

namespace ForgottonChambers.Events
{
    public class EventService
    {
        public EventController OnAnimationFinishedEvent { get; private set; }
        public EventController<WeaponType> OnWeaponPickedUpEvent { get; private set; }
        public EventController OnKeyCollectedEvent { get; private set; }
        public EventController OnDoorOpenedEvent { get; private set; }
        public EventController<int> OnLevelSelected { get; private set; }
        public EventController<int> OnGameStart {  get; private set; }

        public EventController<int> OnPlayerHealthChangedEvent { get; private set; }
        public EventController<int> OnPlayerMaxHealthSetEvent { get; private set; }

        public EventController<bool> OnGameplayUIVisibilityChanged { get; private set; }
        public EventController<bool> OnWeaponUIVisibilityChanged { get; private set; }
        public EventController<bool> OnScoreUIVisibilityChanged { get; private set; }

        public EventController<InstructionData, float> OnShowInstructionEvent { get; private set; }

        public EventController<float> OnScoreAddedEvent { get; private set; }


        public EventService()
        {
            OnGameStart = new EventController<int>();
            OnLevelSelected = new EventController<int>();
            OnAnimationFinishedEvent = new EventController();
            OnWeaponPickedUpEvent = new EventController<WeaponType>();
            OnKeyCollectedEvent = new EventController();
            OnDoorOpenedEvent = new EventController();
            OnPlayerHealthChangedEvent = new EventController<int>();
            OnPlayerMaxHealthSetEvent = new EventController<int>();
            OnGameplayUIVisibilityChanged = new EventController<bool>();
            OnWeaponUIVisibilityChanged = new EventController<bool>();
            OnScoreUIVisibilityChanged = new EventController<bool>();
            OnShowInstructionEvent = new EventController<InstructionData, float>();
            OnScoreAddedEvent = new EventController<float>();
        }
    }
}