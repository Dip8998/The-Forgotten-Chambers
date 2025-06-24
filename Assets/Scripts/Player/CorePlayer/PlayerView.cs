using UnityEngine;
using ForgottonChambers.Weapons;
using ForgottonChambers.ScriptableObjects;
using System.Linq;
using ForgottonChambers.Player.Checks;
using ForgottonChambers.Player.Interactions;
using ForgottonChambers.Player.Components;
using ForgottonChambers.Player.Interfaces;

namespace ForgottonChambers.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerView : MonoBehaviour, IPlayerMover 
    {
        #region Checking Variables
        [Header("Checks")]
        [SerializeField] private GroundCheckConfig groundCheckConfig;
        [SerializeField] private WallCheckConfig wallCheckConfig;
        [SerializeField] private CeilingCheckConfig ceilingCheckConfig;

        [SerializeField] private Transform boxCheck; 
        [SerializeField] private float boxCheckDistance;
        [SerializeField] private LayerMask boxLayer;
        #endregion

        #region Weapons
        [SerializeField] private WeaponView[] _weapons;
        public WeaponView[] Weapons => _weapons;
        #endregion

        #region Internal References
        private PlayerController _playerController; 
        private PlayerPhysicsChecks _physicsChecks;
        private BoxInteractionHandler _boxInteractionHandler;
        private PlayerUnityComponents _unityComponents; 
        #endregion

        #region Other Variables
        private const string BoxTag = "Box";
        #endregion

        public event System.Action OnAnimationFinishedEvent;

        #region Unity Call back functions
        private void Awake()
        {
            _unityComponents = new PlayerUnityComponents(gameObject); 

            _physicsChecks = new PlayerPhysicsChecks(
                groundCheckConfig,
                wallCheckConfig,
                ceilingCheckConfig,
                () => _playerController != null ? _playerController.FacingDirection : 1
            );

            _boxInteractionHandler = new BoxInteractionHandler(
                transform, _unityComponents.Rigidbody, _unityComponents.FixedJoint,
                boxCheckDistance, boxLayer, BoxTag
            );
        }

        private void Start()
        {
            _playerController?.SetupPlayer();
        }

        private void Update()
        {
            _playerController?.OnPlayerUpdate();
            if (_playerController != null)
            {
                _boxInteractionHandler.TryInteractWithBox(
                   _playerController.InputHandler.BoxPushPullInput,
                   _playerController.InputHandler.BoxDropInput,
                   _playerController.InputHandler.MoveInput
                );
            }
        }

        private void FixedUpdate()
        {
            _playerController?.OnPlayerFixedUpdate();
        }
        #endregion

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void OnAnimationFinished()
        {
            OnAnimationFinishedEvent?.Invoke();
        }

        public WeaponView GetWeaponViewByType(WeaponType type)
        {
            return _weapons.FirstOrDefault(w => w.WeaponController.WeaponData.weaponType == type);
        }

        public void SetWeaponGameObjectActive(WeaponType type, bool active)
        {
            WeaponView weapon = GetWeaponViewByType(type);
            if (weapon != null)
            {
                weapon.gameObject.SetActive(active);
            }
        }

        public void SetAnimatorBool(string paramName, bool value) => _unityComponents.SetAnimatorBool(paramName, value);
        public void SetAnimatorFloat(string paramName, float value) => _unityComponents.SetAnimatorFloat(paramName, value);
        public void SetAnimatorTrigger(string paramName) => _unityComponents.SetAnimatorTrigger(paramName);
        public bool GetAnimatorBool(string paramName) => _unityComponents.GetAnimatorBool(paramName); 


        #region Physics Check Methods (Delegated)
        public bool IsCeiling() => _physicsChecks.IsCeiling();
        public bool IsGrounded() => _physicsChecks.IsGrounded();
        public bool IsTouchingWall() => _physicsChecks.IsTouchingWall();
        public bool IsTouchingWallBack() => _physicsChecks.IsTouchingWallBack();
        #endregion

        public bool HasBoxAttached() => _boxInteractionHandler.IsBoxAttached;
        public void DetachBox() => _boxInteractionHandler.DetachBox();

        public BoxCollider2D GetPlayerCollider() => _unityComponents.Collider;

        #region IPlayerMover Implementation (Now uses _unityComponents)
        public void SetLinearVelocity(Vector2 velocity) => _unityComponents.SetRigidbodyLinearVelocity(velocity);
        public void SetVelocityX(float velocity) => _unityComponents.SetRigidbodyLinearVelocity(new Vector2(velocity, _unityComponents.GetRigidbodyLinearVelocity().y));
        public void SetVelocityY(float velocity) => _unityComponents.SetRigidbodyLinearVelocity(new Vector2(_unityComponents.GetRigidbodyLinearVelocity().x, velocity));
        public void SetRotationY(float angle) => _unityComponents.SetRotationY(angle);
        public Vector2 GetCurrentVelocity() => _unityComponents.GetRigidbodyLinearVelocity();
        #endregion

        private void OnDrawGizmos()
        {
            if (groundCheckConfig?.CheckTransform != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheckConfig.CheckTransform.position, groundCheckConfig.Radius);
            }

            if (ceilingCheckConfig?.CheckTransform != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(ceilingCheckConfig.CheckTransform.position, ceilingCheckConfig.CheckTransform.position + Vector3.up * ceilingCheckConfig.Distance);
            }

            if (wallCheckConfig?.CheckTransform != null)
            {
                Gizmos.color = Color.blue;
                if (_playerController != null)
                {
                    Gizmos.DrawRay(wallCheckConfig.CheckTransform.position, Vector2.right * _playerController.FacingDirection * wallCheckConfig.Distance);
                    Gizmos.DrawRay(wallCheckConfig.CheckTransform.position, Vector2.right * -_playerController.FacingDirection * wallCheckConfig.Distance);
                }
                else
                {
                    Gizmos.DrawRay(wallCheckConfig.CheckTransform.position, Vector2.right * wallCheckConfig.Distance);
                    Gizmos.DrawRay(wallCheckConfig.CheckTransform.position, Vector2.left * wallCheckConfig.Distance);
                }
            }

            _boxInteractionHandler?.OnDrawGizmos(transform);
        }
    }
}