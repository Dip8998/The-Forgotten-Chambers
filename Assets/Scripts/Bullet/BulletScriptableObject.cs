using UnityEngine;

namespace ForgottonChambers.Bullets
{
    [CreateAssetMenu(fileName = "NewBulletData", menuName = "ScriptableObjects/BulletData")]
    public class BulletScriptableObject : ScriptableObject
    {
        public float speed = 10f;
        public int damage = 1;
        public float destroyingTime = 3f;
    }
}
