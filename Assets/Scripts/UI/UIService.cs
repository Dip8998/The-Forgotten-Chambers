using ForgottonChambers.Main;
using ForgottonChambers.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ForgottonChambers.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("StartUIView")]
        [SerializeField] private StartUIView startUIVIew;
        public StartUIView StartUIView => startUIVIew;

        [Header("LevelSelectionUI")]
        private LevelSelectionUIController levelSelectionController;
        [SerializeField] private LevelSelectionUIView levelSelectionView;
        public LevelSelectionUIView LevelSelectionUIView => levelSelectionView;

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
            startUIVIew.gameObject.SetActive(false);
            levelSelectionController = new LevelSelectionUIController(levelSelectionView,startUIVIew);
            gameplayUIController = new GameplayUIController(gameplayUIView);

            if(instructionUIView != null)
            {
                instructionUIController = new InstructionUIController(instructionUIView);
            }

            scoreUIController = new ScoreUIController(scoreUIView);
            GameService.Instance.EventService.OnGameStart.AddListener(startUIVIew.ShowLevelSelection);
        }

        public void Show()
        {
            startUIVIew.gameObject.SetActive(true);
        }

        public void InvokStart(int levelCount)
        {
            GameService.Instance.EventService.OnGameStart.InvokeEvent();
            startUIVIew.gameObject.SetActive(false);
        }

        public void SetPlayerHealth(int health) => gameplayUIController.SetPlayerHealth(health);

        public void SetPlayerMaxHealth(int health) => gameplayUIController.SetPlayerMaxHealth(health);

        public void SetPlayerWeaponIcon(WeaponType type) => gameplayUIController.SetWeaponIcon(type);

        public void SetKeyIcon(bool hasKey, bool isActive) => gameplayUIController.SetKeyIcon(hasKey, isActive);

        public void ShowInstructionPanel(InstructionData data, float duration = 4f) => instructionUIController.ShowInstructionPanel(data, duration);

        public void AddScore(float score) => GameService.Instance.EventService.OnScoreAddedEvent?.InvokeEvent(score);

        public void ResetScore() => scoreUIController.ResetScore();

        public void InitializeScoreUI(int levelID) => scoreUIController.InitializeScore(levelID);
    }
}
