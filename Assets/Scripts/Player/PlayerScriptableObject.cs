using UnityEngine;

namespace ForgottonChambers.Player
{
    [CreateAssetMenu(fileName = "NewPlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public PlayerView playerPrefab;
        public float playerMovementSpeed;
        public int playerMaxHealth;
    }
}
