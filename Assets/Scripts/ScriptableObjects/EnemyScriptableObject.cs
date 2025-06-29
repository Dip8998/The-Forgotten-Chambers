using ForgottonChambers.Enemy;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [Header("Prefab & Type")]
        public EnemyView enemyPrefab;
        public EnemyType enemyType;
        public int enemyHealth;

        #region Movement
        [Header("Movement Settings")]
        public float movementSpeed;
        public float chargeSpeed;
        public float castDistance;
        public float groundCheckRadius;
        #endregion

        #region Idle
        [Header("Idle Settings")]
        public float minIdleTime;
        public float maxIdleTime;
        #endregion

        #region Charge
        [Header("Charge Settings")]
        public float chargeTime;
        public float longRangeActionTime;
        #endregion

        #region Detection Ranges
        [Header("Player Detection Ranges")]
        public float minPlayerDetectedDistance;
        public float maxPlayerDetectedDistance;
        public float closeRangeActionDistance;
        #endregion

        #region Attack
        [Header("Attack Settings")]
        public float attackRadius;
        public int attackDamage;
        #endregion

        #region Patrol/Look For Player
        [Header("Patrol Settings")]
        public int amountOfTurns;
        public float timeBetweenTurns;
        #endregion

        #region Knockback
        public float damageHopSpeed;
        public float knockBackSpeed;
        public Vector2 knockBackAngle;
        #endregion

        #region Layers
        [Header("Layer Masks")]
        public LayerMask groundLayer;
        public LayerMask playerLayer;
        #endregion
    }
}
