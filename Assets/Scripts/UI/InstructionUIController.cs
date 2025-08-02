using ForgottonChambers.Events;
using ForgottonChambers.Main;
using ForgottonChambers.ScriptableObjects;

namespace ForgottonChambers.UI
{
    public class InstructionUIController
    {
        private InstructionUIView instructionUIView;

        public InstructionUIController(InstructionUIView instructionUIView)
        {
            this.instructionUIView = instructionUIView;

            instructionUIView.gameObject.SetActive(true);
            GameService.Instance.EventService.OnShowInstructionEvent.AddListener(ShowInstructionPanel);
        }

        public void ShowInstructionPanel(InstructionData data, float duration)
        {
            string instructionText = data?.GetRandomLine() ?? "No instruction text provided.";
            instructionUIView.ShowInstruction(instructionText, duration);
        }
    }
}