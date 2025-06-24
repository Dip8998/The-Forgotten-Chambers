using UnityEngine; 

namespace ForgottonChambers.Player.Interfaces
{
    public interface IPlayerMover
    {
        void SetLinearVelocity(Vector2 velocity);
        void SetVelocityX(float velocity);
        void SetVelocityY(float velocity);
        void SetRotationY(float angle); 
        Vector2 GetCurrentVelocity();
    }
}