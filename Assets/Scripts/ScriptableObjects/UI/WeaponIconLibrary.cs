using System.Collections.Generic;
using UnityEngine;

namespace ForgottonChambers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WeaponIconLibrary", menuName = "ScriptableObjects/WeaponIconLibrary")]
    public class WeaponIconLibrary : ScriptableObject
    {
        public List<WeaponUIData> weaponIcons;

        public Sprite GetIcon(ForgottonChambers.ScriptableObjects.WeaponType type)
        {
            foreach (var icon in weaponIcons)
            {
                if (icon.weaponType == type)
                    return icon.weaponIcon;
            }

            Debug.LogWarning($"No icon found for weapon type {type}");
            return null;
        }
    }
}