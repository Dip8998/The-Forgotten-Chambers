using ForgottonChambers.Box;
using ForgottonChambers.Main;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ForgottonChambers.Interactables
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] private string activatorTag = "Box";
        [SerializeField] private ObstacleGate controlledGate;

        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;

        private SpriteRenderer spriteRenderer;
        private bool isActive = false;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            UpdateSprite();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag(activatorTag))
            {
                if(collision.TryGetComponent(out BoxController box))
                {
                    ActivateSwitch();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(activatorTag))
            {
                if(collision.TryGetComponent(out BoxController box))
                {
                    DeactivateSwitch();
                }
            }
        }

        private void ActivateSwitch()
        {
            if (!isActive)
            {
                isActive = true;
                UpdateSprite();
                GameService.Instance.SoundService.Play(Sound.Sounds.SWITCHACTIVAT);
                controlledGate.OpenGate();
            }
        }

        private void DeactivateSwitch()
        {
            if (isActive)
            {
                isActive = false;
                UpdateSprite();
                controlledGate.CloseGate();
            }
        }

        private void UpdateSprite()
        {
            if (spriteRenderer != null)
            {
                if (isActive && activeSprite != null)
                {
                    spriteRenderer.sprite = activeSprite;
                }
                else if (!isActive && inactiveSprite != null)
                {
                    spriteRenderer.sprite = inactiveSprite;
                }
            }
        }
    }
}