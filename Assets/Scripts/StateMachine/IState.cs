namespace ForgottonChambers.StateMachine
{
    public interface IState
    {
        void OnStateEnter();
        void UpdateState();
        void FixedUpdateState();
        void OnStateExit();
    }
}
