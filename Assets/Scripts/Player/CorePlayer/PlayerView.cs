using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using ForgottonChambers.Box;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace ForgottonChambers.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerView : MonoBehaviour
    {
        [Header("Checks")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private Transform wallCheck;
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private LayerMask wallLayer;

        [SerializeField] private Transform boxCheck;
        [SerializeField] private float boxCheckDistance;
        [SerializeField] private LayerMask boxLayer;

        [SerializeField] private Transform ceilingCheck;
        [SerializeField] private float ceilingCheckDistance;

        public Animator PlayerAnimator { get; private set; }
        private PlayerController _playerController;
        private GameObject _attachedBox;
        private Rigidbody2D _playerRb;

        private FixedJoint2D _boxFixedJoint;

        private const string BoxTag = "Box";

        private void Awake()
        {
            PlayerAnimator = GetComponent<Animator>();
            _playerRb = GetComponent<Rigidbody2D>();
            _boxFixedJoint = GetComponent<FixedJoint2D>();

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
        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void OnAnimationFinished()
        {
            _playerController?.AnimationFinishedTrigger();
        }

        public bool IsCeiling() =>
            Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, groundLayer);

        public bool IsGrounded() =>
            Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        public bool IsTouchingWall() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * _playerController.FacingDirection, wallCheckDistance, wallLayer);

        public bool IsTouchingWallBack() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * -_playerController.FacingDirection, wallCheckDistance, wallLayer);

        private void HandleBoxInteraction()
        {
            Physics2D.queriesStartInColliders = false;

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                Vector2.right * transform.localScale.x,
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

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            if (ceilingCheck != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * ceilingCheckDistance);
            }

            if (wallCheck != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(wallCheck.position, Vector2.right * wallCheckDistance);
                Gizmos.DrawRay(wallCheck.position, Vector2.left * wallCheckDistance);
            }

            if (boxCheck != null)
            {
                Gizmos.color = Color.yellow;

                Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * boxCheckDistance);
            }
        }
    }
}
