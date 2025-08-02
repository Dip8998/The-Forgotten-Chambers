using UnityEngine;
using ForgottonChambers.Player;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [Header("Prefabs")]
        public PlayerView playerPrefab;

        [Header("Movement")]
        public float playerMovementSpeed;
        public float playerJumpForce;
        public float playerDoubleJumpForce;
        public float playerCrouchMovementSpeed;

        [Header("Collision & Sizing")]
        public Vector2 playerStandColliderSize = new Vector2(1.0f, 1.8f);
        public Vector2 playerStandColliderOffset = new Vector2(0.0f, 0.0f);
        public Vector2 playerCrouchColliderSize = new Vector2(1.0f, 0.5f);
        public Vector2 playerCrouchColliderOffset = new Vector2(0.0f, -0.25f);

        [Header("Wall Interaction")]
        public float playerWallJumpSpeed;
        public float playerWallJumpTime;
        public Vector2 playerWallJumpAngle = new Vector2(1, 2);

        [Header("Time & Jumps")]
        public float coyoteTime;
        public int amountOfJumps;

        [Header("Combat")]
        public int playerMaxHealth;
        public float attackCooldownTime = 0.5f;
    }
}