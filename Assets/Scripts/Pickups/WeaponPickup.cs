using UnityEngine;
using ForgottonChambers.ScriptableObjects;
using ForgottonChambers.Player;
using ForgottonChambers.Main;

namespace ForgottonChambers.Pickups
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponType weaponType;
        [SerializeField] private GameObject crabEnemy;
        [SerializeField] private Transform crabSpawnPos;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerView playerView))
            {
                GameService.Instance.EventService.OnWeaponPickedUpEvent.InvokeEvent(weaponType);

                if (crabEnemy != null && crabSpawnPos != null)
                {
                    Instantiate(crabEnemy, crabSpawnPos.position, Quaternion.identity);
                }

                GameService.Instance.SoundService.Play(Sound.Sounds.WEAPONPICKUP);

                Destroy(gameObject);
            }
        }
    }
}