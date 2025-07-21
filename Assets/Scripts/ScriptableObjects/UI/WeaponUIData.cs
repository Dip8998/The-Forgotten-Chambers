using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponUIData", menuName = "ScriptableObjects/WeaponUIData")]
    public class WeaponUIData : ScriptableObject
    {
        public WeaponType weaponType;
        public Sprite weaponIcon;
    }
}