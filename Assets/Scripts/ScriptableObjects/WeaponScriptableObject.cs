using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public WeaponType weaponType;
        public int attackDamage;
        public float[] attackMovementSpeeds;
    }
}