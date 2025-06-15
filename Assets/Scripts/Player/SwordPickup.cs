using UnityEngine;

namespace ForgottonChambers.Player
{
    public class SwordPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView player))
            {
                player.SetHasSword(true);
                Destroy(gameObject); 
            }
        }
    }
}
