using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Player;

namespace ForgottonChambers.Weapons
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                playerView.PlayerController?.AddWeaponToInventory(weaponType);
                Destroy(gameObject);
            }
        }
    }
}
