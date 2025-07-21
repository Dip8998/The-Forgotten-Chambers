namespace ForgottonChambers.KeyandDoor
{
    public class KeyAndDoorModel
    {
        public bool PlayerHasKey { get; private set; }

        public void CollectKey()
        {
            PlayerHasKey = true;
        }

        public void UseKey()
        {
            PlayerHasKey = false;
        }
    }
}
