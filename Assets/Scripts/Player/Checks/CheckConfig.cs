using UnityEngine; 

namespace ForgottonChambers.Player.Checks
{
    [System.Serializable]
    public class GroundCheckConfig
    {
        public Transform CheckTransform;
        public float Radius = 0.2f;
        public LayerMask Layer;
    }

    [System.Serializable]
    public class WallCheckConfig
    {
        public Transform CheckTransform;
        public float Distance;
        public LayerMask Layer;
    }

    [System.Serializable]
    public class CeilingCheckConfig
    {
        public Transform CheckTransform;
        public float Distance;
        public LayerMask Layer; 
    }
}