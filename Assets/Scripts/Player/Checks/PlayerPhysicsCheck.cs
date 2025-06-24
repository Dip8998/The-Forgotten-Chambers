using System;
using UnityEngine;

namespace ForgottonChambers.Player.Checks
{
    public class PlayerPhysicsChecks
    {
        private  GroundCheckConfig _groundConfig;
        private  WallCheckConfig _wallConfig;
        private CeilingCheckConfig _ceilingConfig;

        private System.Func<int> _getFacingDirection;

        public PlayerPhysicsChecks(GroundCheckConfig groundConfig, WallCheckConfig wallConfig, CeilingCheckConfig ceilingConfig, Func<int> getFacingDirection)
        {
            _groundConfig = groundConfig;
            _wallConfig = wallConfig;
            _ceilingConfig = ceilingConfig;
            _getFacingDirection = getFacingDirection;
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundConfig.CheckTransform.position, _groundConfig.Radius, _groundConfig.Layer);
        }

        public bool IsTouchingWall()
        {
            return Physics2D.Raycast(_wallConfig.CheckTransform.position, Vector2.right * _getFacingDirection(), _wallConfig.Distance, _wallConfig.Layer);
        }

        public bool IsTouchingWallBack()
        {
            return Physics2D.Raycast(_wallConfig.CheckTransform.position, Vector2.right * -_getFacingDirection(), _wallConfig.Distance, _wallConfig.Layer);
        }

        public bool IsCeiling()
        {
            return Physics2D.Raycast(_ceilingConfig.CheckTransform.position, Vector2.up, _ceilingConfig.Distance, _ceilingConfig.Layer);
        }
    }
}