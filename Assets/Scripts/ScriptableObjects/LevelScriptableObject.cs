using ForgottonChambers.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Level
{
    [CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public int ID;
        public GameObject LevelPrefab;
    }
}