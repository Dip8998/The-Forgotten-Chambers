using UnityEngine;

namespace ForgottonChambers.Player
{
    public class GunPickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out PlayerView player))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

