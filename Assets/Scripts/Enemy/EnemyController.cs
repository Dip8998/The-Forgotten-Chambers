using ForgottonChambers.ScriptableObjects;
using UnityEngine;

namespace ForgottonChambers.Enemy
{
    public class EnemyController
    {
        public EnemyView EnemyView { get; private set; }
        private EnemyScriptableObject enemyData;
        private EnemyStateMachine stateMachine;

        public EnemyIdleState IdleState { get; private set; }
        public EnemyMoveState MoveState { get; private set; }
        public EnemyPlayerDetectedState PlayerDetectedState { get; private set; }
        public EnemyChargeState ChargeState { get; private set; }
        public EnemyLookForPlayerState LookForPlayerState { get; private set; }

        private Vector2 velocityWorkSpace;

        public int FacingDirection { get; private set; }

        public EnemyController(EnemyView enemyView, EnemyScriptableObject enemyData)
        {
            FacingDirection = 1;
            this.EnemyView = enemyView;
            this.enemyData = enemyData;

            enemyView.SetEnemyController(this);

            stateMachine = new EnemyStateMachine();
            InitializeStates();
            stateMachine.Initialize(MoveState);
        }

        private void InitializeStates()
        {
            IdleState = new EnemyIdleState(stateMachine, this, enemyData, "Idle");
            MoveState = new EnemyMoveState(stateMachine, this, enemyData, "Move");
            PlayerDetectedState = new EnemyPlayerDetectedState(stateMachine, this, enemyData, "PlayerDetected");
            ChargeState = new EnemyChargeState(stateMachine, this, enemyData, "Charge");
            LookForPlayerState = new EnemyLookForPlayerState(stateMachine, this, enemyData, "LookForPlayer");
        }

        public void UpdateController()
        {
            stateMachine.CurrentState.OnUpdate();
        }

        public void FixedUpdateController()
        {
            stateMachine.CurrentState.OnFixedUpdate();
        }

        public void SetVelocity(float speed)
        {
            velocityWorkSpace.Set(FacingDirection * speed, EnemyView.Rigidbody.linearVelocity.y);
            EnemyView.Rigidbody.linearVelocity = velocityWorkSpace;
        }

        public bool CheckIsHittingWall()
        {
            float castDist = enemyData.castDistance * FacingDirection;
            Vector3 target = EnemyView.CastPosition.position + new Vector3(castDist, 0, 0);

            Debug.DrawLine(EnemyView.CastPosition.position, target, Color.blue);
            return Physics2D.Linecast(EnemyView.CastPosition.position, target, enemyData.groundLayer);
        }

        public bool CheckIsNearEdge()
        {
            Vector3 target = EnemyView.CastPosition.position + new Vector3(0, -enemyData.castDistance, 0);

            Debug.DrawLine(EnemyView.CastPosition.position, target, Color.red);
            return !Physics2D.Linecast(EnemyView.CastPosition.position, target, enemyData.groundLayer);
        }

        public bool CheckIsPlayerInMinRange()
        {
            Vector3 target = EnemyView.PlayerCheck.position + new Vector3(enemyData.minPlayerDetectedDistance * FacingDirection, 0, 0);
            Debug.DrawLine(EnemyView.PlayerCheck.position, target, Color.green);
            return Physics2D.Linecast(EnemyView.PlayerCheck.position, target, enemyData.playerLayer);
        }

        public bool CheckIsPlayerInMaxRange()
        {
            Vector3 target = EnemyView.PlayerCheck.position + new Vector3(enemyData.maxPlayerDetectedDistance * FacingDirection, 0, 0);
            Debug.DrawLine(EnemyView.PlayerCheck.position, target, Color.green);
            return Physics2D.Linecast(EnemyView.PlayerCheck.position, target, enemyData.playerLayer);
        }

        public void Flip()
        {
            FacingDirection *= -1;
            EnemyView.FlipDirection(FacingDirection == 1);
        }
    }
}
