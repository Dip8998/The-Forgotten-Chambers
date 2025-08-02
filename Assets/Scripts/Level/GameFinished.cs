using ForgottonChambers.Main;
using ForgottonChambers.Player;
using System.Collections;
using UnityEngine;

namespace ForgottonChambers.Level
{
    public class GameFinished : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out PlayerView player))
            {
                GameService.Instance.StartCoroutine(GameWinPanel());
            }
        }

        private IEnumerator GameWinPanel()
        {
            yield return new WaitForSeconds(2f);
            GameService.Instance.GameWinUIView.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}