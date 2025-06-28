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
        public EnemyMeleeAttackState MeleeAttackState { get; private set; }

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
            MeleeAttackState = new EnemyMeleeAttackState(stateMachine, this, enemyData, "MeleeAttack");
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

        public bool CheckIsHittingWall() => AllChecks(EnemyView.CastPosition, enemyData.castDistance, 0, Color.blue, enemyData.groundLayer);

        public bool CheckIsNearEdge() => !AllChecks(EnemyView.CastPosition, 0, -enemyData.castDistance, Color.red, enemyData.groundLayer);

        public bool CheckIsPlayerInMinRange() => AllChecks(EnemyView.PlayerCheck, enemyData.minPlayerDetectedDistance, 0, Color.green, enemyData.playerLayer);

        public bool CheckIsPlayerInMaxRange() => AllChecks(EnemyView.PlayerCheck, enemyData.maxPlayerDetectedDistance, 0, Color.green, enemyData.playerLayer);

        public bool CheckIsPlayerInCloseRange() => AllChecks(EnemyView.PlayerCheck, enemyData.closeRangeActionDistance, 0, Color.yellow, enemyData.playerLayer);

        private bool AllChecks(Transform transform, float DistanceX, float DistanceY, Color color, LayerMask layer)
        {
            Vector3 target = transform.position + new Vector3(DistanceX * FacingDirection, DistanceY, 0);
            Debug.DrawLine(transform.position, target, color);
            return Physics2D.Linecast(transform.position, target, layer);
        }

        public void Flip()
        {
            FacingDirection *= -1;
            EnemyView.FlipDirection(FacingDirection == 1);
        }

        public void AnimationAttackTrigger()
        {
            MeleeAttackState.AnimationAttackTrigger();
        }

        public void AnimationFinishedTrigger()
        {
            MeleeAttackState.AnimationFinishedTrigger();
        }
    }
}
