using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeaponScriptableObject", menuName = "ScriptableObjects/WeaponScriptableObject")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public float[] movementSpeed;
    }
}
