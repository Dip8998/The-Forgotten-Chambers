using ForgottonChambers.Main;
using ForgottonChambers.Player;
using ForgottonChambers.Weapons;
using UnityEngine;

namespace ForgottonChambers.Interactables
{
    public class ObstacleGate : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Collider2D gateCollider;
        private bool isOpen = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            gateCollider = GetComponent<Collider2D>();

            CloseGate();
        }

        public void OpenGate()
        {
            if (!isOpen) 
            {
                isOpen = true;
                spriteRenderer.enabled = false;
                gateCollider.enabled = false;
            }
        }

        public void CloseGate()
        {
            if (isOpen) 
            {
                isOpen = false;
                spriteRenderer.enabled = true;
                gateCollider.enabled = true;
            }
        }
    }
}
