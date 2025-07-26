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
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
                if (gateCollider != null)
                {
                    gateCollider.enabled = false;
                }
            }
        }

        public void CloseGate()
        {
            if (isOpen) 
            {
                isOpen = false;
                if(spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
                if(gateCollider != null)
                {
                    gateCollider.enabled = true;
                }
            }
        }
    }
}
