using ForgottonChambers.Main;
using ForgottonChambers.Player;
using UnityEngine;

public class EnvironmentalDeath : MonoBehaviour
{
    private int instantDeathDamage = 20;
    [SerializeField] private GameObject gamePlayView;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerView player))
        {
            gamePlayView.SetActive(true);
            player.PlayerController.Damage(instantDeathDamage);
            GameService.Instance.StartCoroutine(player.PlayerController.Respawn());
        }
        else if(collision.TryGetComponent(out EnemyView enemy))
        {
            enemy.Controller.Die();
        }
    }
}
