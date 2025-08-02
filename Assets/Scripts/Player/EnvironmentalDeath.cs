using ForgottonChambers.Main;
using ForgottonChambers.Player;
using ForgottonChambers.UI;
using UnityEngine;

public class EnvironmentalDeath : MonoBehaviour
{
    private int instantDeathDamage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerView player))
        {
            player.PlayerController.Damage(instantDeathDamage);
        }
        else if(collision.TryGetComponent(out EnemyView enemy))
        {
            enemy.Controller.Die();
        }
    }
}
