namespace ForgottonChambers.KeyandDoor
{
    public class KeyAndDoorController
    {
        private KeyAndDoorModel _model;

        public KeyAndDoorController(KeyAndDoorModel model)
        {
            _model = model;
        }

        public void CollectKey()
        {
            _model.CollectKey();
        }

        public bool HasKey() => _model.PlayerHasKey;

        public void UseKey()
        {
            _model.UseKey();
        }
    }
}
