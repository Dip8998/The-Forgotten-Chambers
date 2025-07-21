using UnityEngine;
using TMPro;
using System.Collections;

namespace ForgottonChambers.UI
{
    public class InstructionUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI instructionText;
        [SerializeField] private GameObject instructionPanel;

        private void Awake()
        {
            instructionPanel.SetActive(false);
        }

        public void ShowInstruction(string text, float duration)
        {
            StopAllCoroutines();
            instructionText.text = text;
            instructionPanel.SetActive(true);
            StartCoroutine(HideAfterSeconds(duration));
        }

        private IEnumerator HideAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            instructionPanel.SetActive(false);
        }
    }
}