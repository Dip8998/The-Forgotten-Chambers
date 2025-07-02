using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Player;
using ForgottonChambers.Main;

namespace ForgottonChambers.Pickups
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                GameService.Instance.EventService.OnWeaponPickedUpEvent.InvokeEvent(weaponType);
                Destroy(gameObject);
            }
        }
    }
}