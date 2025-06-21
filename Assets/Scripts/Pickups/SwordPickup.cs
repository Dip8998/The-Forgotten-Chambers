using ForgottonChambers.Player;
using UnityEngine;

namespace ForgottonChambers.Pickups
{
    public class SwordPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerView player))
            {
                Destroy(gameObject); 
            }
        }
    }
}
