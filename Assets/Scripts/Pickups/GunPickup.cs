using ForgottonChambers.Player;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Weapons;
using UnityEngine;

namespace ForgottonChambers.Pickups
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

