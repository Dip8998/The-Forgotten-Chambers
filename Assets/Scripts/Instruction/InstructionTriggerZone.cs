using ForgottonChambers.Player;
using ForgottonChambers.Events;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using UnityEditor.MPE;
using ForgottonChambers.Main;

namespace ForgottonChambers.Instruction
{
    [RequireComponent(typeof(Collider2D))]
    public class InstructionTriggerZone : MonoBehaviour
    {
        [SerializeField] private InstructionData instructionData;
        [SerializeField] private float displayDuration = 4f;

        private bool triggered = false;

        protected virtual void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (triggered) return;

            if (other.TryGetComponent(out PlayerView playerView))
            {
                if (instructionData != null)
                {
                    GameService.Instance.EventService.OnShowInstructionEvent.InvokeEvent(instructionData, displayDuration);
                    triggered = true;
                }
            }
        }
    }
}