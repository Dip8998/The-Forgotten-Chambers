using UnityEngine;

namespace ForgottonChambers.Player
{
    [CreateAssetMenu(fileName = "NewPlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public PlayerView playerPrefab;
        public float playerMovementSpeed;
        public float playerJumpForce;
        public float playerDoubleJumpForce;
        public float playerCrouchMovementSpeed;
        public float playerWallSlideSpeed;
        public float playerWallClimbSpeed;
        public float playerWallJumpSpeed;
        public float playerWallJumpTime;
        public Vector2 playerWallJumpAngle = new Vector2(1,2); 
        public float coyoteTime;
        public int playerMaxHealth;
        public int amountOfJumps;
    }
}
