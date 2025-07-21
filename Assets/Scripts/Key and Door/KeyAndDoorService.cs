using ForgottonChambers.Events;
using ForgottonChambers.Main;

namespace ForgottonChambers.KeyandDoor
{
    public class KeyAndDoorService
    {
        private KeyAndDoorModel _model;
        private KeyAndDoorController _controller;

        public KeyAndDoorController Controller => _controller;

        public KeyAndDoorService()
        {
            _model = new KeyAndDoorModel();
            _controller = new KeyAndDoorController(_model);
        }

        public void OnKeyCollected()
        {
            _controller.CollectKey();
            GameService.Instance.EventService.OnKeyCollectedEvent.InvokeEvent();
        }

        public bool CanOpenDoor()
        {
            return _controller.HasKey();
        }

        public void UseKey()
        {
            _controller.UseKey();
            GameService.Instance.EventService.OnDoorOpenedEvent.InvokeEvent();
        }
    }
}
