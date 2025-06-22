using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "ScriptableObjects/WeaponData")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public float[] attackMovementSpeeds;
    }
}