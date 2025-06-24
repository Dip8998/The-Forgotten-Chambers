using UnityEngine;
using ForgottonChambers.Box; 

namespace ForgottonChambers.Player.Interactions
{
    public class BoxInteractionHandler
    {
        private Transform _playerTransform;
        private Rigidbody2D _playerRb;
        private FixedJoint2D _boxFixedJoint;
        private float _boxCheckDistance;
        private LayerMask _boxLayer;
        private string _boxTag;

        private GameObject _attachedBox;

        public bool IsBoxAttached => _attachedBox != null;

        public BoxInteractionHandler(Transform playerTransform, Rigidbody2D playerRb, FixedJoint2D boxFixedJoint,
                                     float boxCheckDistance, LayerMask boxLayer, string boxTag)
        {
            _playerTransform = playerTransform;
            _playerRb = playerRb;
            _boxFixedJoint = boxFixedJoint;
            _boxCheckDistance = boxCheckDistance;
            _boxLayer = boxLayer;
            _boxTag = boxTag;

            if (_boxFixedJoint == null)
            {
                Debug.LogError("FixedJoint2D is required for BoxInteractionHandler but is null.");
            }
        }

        public void TryInteractWithBox(bool pushPullInput, bool dropInput, float playerMoveInput)
        {
            Physics2D.queriesStartInColliders = false;

            RaycastHit2D hit = Physics2D.Raycast(
                _playerTransform.position,
                Vector2.right * _playerTransform.localScale.x,
                _boxCheckDistance,
                _boxLayer
            );

            if (hit.collider != null && hit.collider.CompareTag(_boxTag) && pushPullInput)
            {
                AttachBox(hit.collider.gameObject);
            }
            else if (dropInput)
            {
                DetachBox();
            }

            if (_attachedBox != null && Mathf.Abs(_playerRb.linearVelocity.x) > 0.01f && Mathf.Approximately(playerMoveInput, 0))
            {
                _playerRb.linearVelocity = new Vector2(0, _playerRb.linearVelocity.y);
            }
        }

        private void AttachBox(GameObject boxObject)
        {
            if (_boxFixedJoint == null) return;

            _attachedBox = boxObject;
            var joint = _attachedBox.GetComponent<FixedJoint2D>();
            if (joint == null) 
            {
                joint = _attachedBox.AddComponent<FixedJoint2D>();
            }

            joint.connectedBody = _playerRb;
            joint.autoConfigureConnectedAnchor = false; 
            joint.enabled = true;

            var boxScript = _attachedBox.GetComponent<boxpull>();
            if (boxScript != null)
            {
                boxScript.beingPushed = true;
            }
        }

        public void DetachBox()
        {
            if (_attachedBox == null) return;

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

        public void OnDrawGizmos(Transform playerTransform)
        {
            if (_playerTransform == null) return; 

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(playerTransform.position, (Vector2)playerTransform.position + Vector2.right * playerTransform.localScale.x * _boxCheckDistance);
        }
    }
}