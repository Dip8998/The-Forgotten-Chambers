using ForgottonChambers.Main;

namespace ForgottonChambers.UI
{
    public class ScoreUIController
    {
        private ScoreUIView scoreUIView;

        public ScoreUIController(ScoreUIView scoreUIView)
        {
            this.scoreUIView = scoreUIView;

            GameService.Instance.EventService.OnScoreAddedEvent.AddListener(AddScore);
            GameService.Instance.EventService.OnScoreUIVisibilityChanged.AddListener(scoreUIView.SetScoreUIVisibility);
        }

        public void AddScore(float score) => scoreUIView.AddScore(score);
    }
}
