using ForgottonChambers.Enemy;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public EnemyView enemyPrefab;
        public EnemyType enemyType;
        public float movementSpeed;
        public float chargeSpeed;
        public float minIdleTime;
        public float maxIdleTime;
        public float chargeTime;
        public float longRangeActionTime;
        public float minPlayerDetectedDistance;
        public float maxPlayerDetectedDistance;
        public float closeRangeActionDistance;
        public float attackRadius;
        public float castDistance;
        public int amountOfTurns;
        public int attackDamage;
        public float timeBetweenTurns;
        public LayerMask groundLayer;
        public LayerMask playerLayer;
    }
}