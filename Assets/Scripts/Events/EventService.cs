using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.Events
{
    public class EventService
    {
        public EventController OnAnimationFinishedEvent { get; private set; }
        public EventController<WeaponType> OnWeaponPickedUpEvent { get; private set; }

        public EventService()
        {
            OnAnimationFinishedEvent = new EventController();
            OnWeaponPickedUpEvent = new EventController<WeaponType>();
        }
    }
}