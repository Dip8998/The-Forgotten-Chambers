using UnityEngine;
using TMPro;
using ForgottonChambers.Main;

namespace ForgottonChambers.KeyandDoor
{
    public class DoorView : MonoBehaviour
    {
        [SerializeField] private GameObject canvasDoor;
        [SerializeField] private TextMeshProUGUI keyRequiredText;

        private void Start()
        {
            if (canvasDoor != null)
                canvasDoor.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player.PlayerView playerView))
            {
                if (GameService.Instance.KeyAndDoorService.CanOpenDoor())
                {
                    GameService.Instance.KeyAndDoorService.UseKey();
                    Destroy(gameObject);
                }
                else
                {
                    canvasDoor?.SetActive(true);
                    if (keyRequiredText != null)
                        keyRequiredText.text = "Find the Key!";
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player.PlayerView playerView))
            {
                canvasDoor?.SetActive(false);
            }
        }
    }
}
