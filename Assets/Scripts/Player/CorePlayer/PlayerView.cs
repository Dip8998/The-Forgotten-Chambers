using UnityEngine;
using ForgottonChambers.Weapons;
using ForgottonChambers.ScriptableObjects;
using System.Linq;
using ForgottonChambers.Player.Checks;
using ForgottonChambers.Player.Components;
using ForgottonChambers.Player.Interfaces;
using ForgottonChambers.Main;
using UnityEngine.UIElements;
using ForgottonChambers.Box;

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
        private PlayerUnityComponents _unityComponents;
        private GameObject _attachedBox;
        private Rigidbody2D _playerRb;
        private FixedJoint2D _boxFixedJoint;
        #endregion

        #region Other Variables
        public PlayerController PlayerController => _playerController;
        private const string BoxTag = "Box";
        #endregion

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
            _playerRb = _unityComponents.Rigidbody;
            _boxFixedJoint = _unityComponents.FixedJoint;

            if (_boxFixedJoint == null)
            {
                _boxFixedJoint = gameObject.AddComponent<FixedJoint2D>();
                _boxFixedJoint.autoConfigureConnectedAnchor = false;
            }

            _boxFixedJoint.enabled = false;
        }

        private void Start()
        {
            _playerController?.SetupPlayer();
        }

        private void Update()
        {
            _playerController?.OnPlayerUpdate();
            HandleBoxInteraction();
        }

        private void FixedUpdate()
        {
            _playerController?.OnPlayerFixedUpdate();
            if (_attachedBox != null && Mathf.Abs(_playerRb.linearVelocity.x) > 0.01f && Mathf.Approximately(_playerController.InputHandler.MoveInput, 0))
            {
                _playerRb.linearVelocity = new Vector2(0, _playerRb.linearVelocity.y);
            }

        }
        #endregion

        #region Setting Player
        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }
        #endregion

        #region Weapon Setup Functions
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
        #endregion

        #region Animation Functions
        public void SetAnimatorBool(string paramName, bool value) => _unityComponents.SetAnimatorBool(paramName, value);
        public void SetAnimatorFloat(string paramName, float value) => _unityComponents.SetAnimatorFloat(paramName, value);
        public void SetAnimatorTrigger(string paramName) => _unityComponents.SetAnimatorTrigger(paramName);
        public bool GetAnimatorBool(string paramName) => _unityComponents.GetAnimatorBool(paramName);

        public void OnAnimationFinished()
        {
            GameService.Instance.EventService.OnAnimationFinishedEvent.InvokeEvent();
        }

        #endregion

        #region Physics Check Methods (Delegated)
        public bool IsCeiling() => _physicsChecks.IsCeiling();
        public bool IsGrounded() => _physicsChecks.IsGrounded();
        public bool IsTouchingWall() => _physicsChecks.IsTouchingWall();
        public bool IsTouchingWallBack() => _physicsChecks.IsTouchingWallBack();
        #endregion

        #region Collider Functions
        private void HandleBoxInteraction()
        {
            Physics2D.queriesStartInColliders = false;

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.right * _playerController.FacingDirection,
                boxCheckDistance,
                boxLayer
            );

            if (hit.collider != null && hit.collider.CompareTag("Box") && _playerController.InputHandler.BoxPushPullInput)
            {
                _attachedBox = hit.collider.gameObject;

                var joint = _attachedBox.GetComponent<FixedJoint2D>();
                joint.connectedBody = _playerRb;
                joint.enabled = true;

                _attachedBox.GetComponent<boxpull>().beingPushed = true;
            }
            else if (_playerController.InputHandler.BoxDropInput)
            {
                if (_attachedBox != null)
                {
                    var joint = _attachedBox.GetComponent<FixedJoint2D>();
                    var boxScript = _attachedBox.GetComponent<boxpull>();

                    if (joint != null)
                    {
                        joint.connectedBody = null;
                        joint.enabled = false;
                    }

                    if (boxScript != null)
                    {
                        boxScript.beingPushed = false;
                    }

                    _attachedBox = null;

                    _playerRb.linearVelocity = new Vector2(0f, _playerRb.linearVelocity.y);
                    _playerRb.angularVelocity = 0f;
                }
            }
        }



        public bool HasBoxAttached() => _attachedBox != null;
        public BoxCollider2D GetPlayerCollider() => _unityComponents.Collider;
        #endregion

        #region IPlayerMover Implementation (Now uses _unityComponents)
        public void SetLinearVelocity(Vector2 velocity) => _unityComponents.SetRigidbodyLinearVelocity(velocity);
        public void SetVelocityX(float velocity) => _unityComponents.SetRigidbodyLinearVelocity(new Vector2(velocity, _unityComponents.GetRigidbodyLinearVelocity().y));
        public void SetVelocityY(float velocity) => _unityComponents.SetRigidbodyLinearVelocity(new Vector2(_unityComponents.GetRigidbodyLinearVelocity().x, velocity));
        public void SetRotationY(float angle) => _unityComponents.SetRotationY(angle);
        public Vector2 GetCurrentVelocity() => _unityComponents.GetRigidbodyLinearVelocity();
        #endregion

        #region OnDrawGizmos
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

            if (boxCheck != null)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * boxCheckDistance);
            }
        }
        #endregion
    }
}