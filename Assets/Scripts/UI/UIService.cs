using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace ForgottonChambers.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("GameplayUI")]
        private GameplayUIController gameplayUIController;
        [SerializeField] private GameplayUIView gameplayUIView;

        [Header("InstructionUI")]
        private InstructionUIController instructionUIController;
        [SerializeField] private InstructionUIView instructionUIView;

        [Header("ScoreUI")]
        private ScoreUIController scoreUIController;
        [SerializeField] private ScoreUIView scoreUIView;

        public void Initialize()
        {
            gameplayUIController = new GameplayUIController(gameplayUIView);
            instructionUIController = new InstructionUIController(instructionUIView);
            scoreUIController = new ScoreUIController(scoreUIView);
        }

        public void SetPlayerHealth(int health) => gameplayUIController.SetPlayerHealth(health);

        public void SetPlayerMaxHealth(int health) => gameplayUIController.SetPlayerMaxHealth(health);

        public void SetPlayerWeaponIcon(WeaponType type) => gameplayUIController.SetWeaponIcon(type);

        public void SetKeyIcon(bool hasKey, bool isActive) => gameplayUIController.SetKeyIcon(hasKey, isActive);

        public void ShowInstructionPanel(InstructionData data, float duration = 4f)
        {
            string selectedLine = data.GetRandomLine();
            instructionUIView.ShowInstruction(selectedLine, duration);
        }

        public void AddScore(float score) => scoreUIController.AddScore(score);
    }
}
