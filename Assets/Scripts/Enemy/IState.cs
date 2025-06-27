
namespace ForgottonChambers.Enemy
{
    public interface IState
    {
        EnemyController Owner { get; set; }

        void OnStateEnter();
        void Update();
        void OnStateExit();
    }

    public enum States
    {
        IDLE,
        PATROLLING,
        CHASING,
        ATTACKING,
        SHOOTING,
        TELEPORTING,
        CLONING
    }
}
