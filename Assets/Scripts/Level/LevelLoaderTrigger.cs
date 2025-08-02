using UnityEngine;
using ForgottonChambers.Main;
using ForgottonChambers.UI;
using ForgottonChambers.Player;
using System.Collections;

namespace ForgottonChambers.Level
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelLoaderTrigger : MonoBehaviour
    {
        [SerializeField] private int levelID;
        [SerializeField] private int nextLevelToLoadID;

        private bool hasLoaded = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasLoaded) return;

            if (collision.TryGetComponent(out PlayerView player))
            {
                hasLoaded = true;

                Debug.Log($"Player entered loader trigger for Level ID: {levelID}. Marking as complete and setting next level to: {nextLevelToLoadID}");

                GameService.Instance.LevelService.MarkLevelComplete(levelID);

                GameService.Instance.StartCoroutine(WinPanel());
            }
        }

        private IEnumerator WinPanel()
        {
            yield return new WaitForSeconds(0.5f);
            GameWinUIView gameWinUIView = GameService.Instance.LevelWinUIView;

            gameWinUIView.SetNextLevelId(nextLevelToLoadID);

            gameWinUIView.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}