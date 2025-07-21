using ForgottonChambers.Player;
using UnityEngine;

namespace ForgottonChambers.Traps
{
    public class BridgeTraps : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out PlayerView player))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
