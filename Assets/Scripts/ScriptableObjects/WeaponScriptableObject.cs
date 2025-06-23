using ForgottonChambers.Weapons;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public WeaponType weaponType;
        public float[] attackMovementSpeeds;
    }

    public enum WeaponType
    {
        Sword,
        Punch,
        Gun
    }
}