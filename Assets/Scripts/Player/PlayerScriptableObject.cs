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
        public float coyoteTime;
        public int playerMaxHealth;
        public int amountOfJumps;
    }
}
