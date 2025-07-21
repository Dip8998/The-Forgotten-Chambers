using ForgottonChambers.Player;
using ForgottonChambers.Events;
using UnityEngine;

namespace ForgottonChambers.Instruction
{
    public class HealthAndScoreTrigegrZone : InstructionTriggerZone
    {
        protected override void Awake()
        {
            base.Awake();

            Main.GameService.Instance.EventService?.OnGameplayUIVisibilityChanged.InvokeEvent(false);
            Main.GameService.Instance.EventService?.OnWeaponUIVisibilityChanged.InvokeEvent(false);
            Main.GameService.Instance.EventService?.OnScoreUIVisibilityChanged.InvokeEvent(false);
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.TryGetComponent(out PlayerView playerView))
            {
                Main.GameService.Instance.EventService?.OnGameplayUIVisibilityChanged.InvokeEvent(true);
                Main.GameService.Instance.EventService?.OnWeaponUIVisibilityChanged.InvokeEvent(true);
                Main.GameService.Instance.EventService?.OnScoreUIVisibilityChanged.InvokeEvent(true);
            }
        }
    }
}