using UnityEngine;

namespace ForgottonChambers.Player.Components
{
    public class PlayerUnityComponents
    {
        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public BoxCollider2D Collider { get; private set; }
        public FixedJoint2D FixedJoint { get; private set; }

        public PlayerUnityComponents(GameObject playerGameObject)
        {
            Animator = playerGameObject.GetComponent<Animator>();
            Rigidbody = playerGameObject.GetComponent<Rigidbody2D>();
            Collider = playerGameObject.GetComponent<BoxCollider2D>();
            FixedJoint = playerGameObject.GetComponent<FixedJoint2D>();

            if (FixedJoint == null)
            {
                FixedJoint = playerGameObject.AddComponent<FixedJoint2D>();
                FixedJoint.autoConfigureConnectedAnchor = false;
            }
            FixedJoint.enabled = false;
        }

        public void SetAnimatorBool(string paramName, bool value) => Animator.SetBool(paramName, value);
        public void SetAnimatorFloat(string paramName, float value) => Animator.SetFloat(paramName, value);
        public void SetAnimatorTrigger(string paramName) => Animator.SetTrigger(paramName);
        public bool GetAnimatorBool(string paramName) => Animator.GetBool(paramName); 
        public void SetRigidbodyLinearVelocity(Vector2 velocity) => Rigidbody.linearVelocity = velocity;
        public Vector2 GetRigidbodyLinearVelocity() => Rigidbody.linearVelocity;
        public void SetColliderSize(Vector2 size, Vector2 offset) { Collider.size = size; Collider.offset = offset; }
        public void SetRotationY(float angle) => Rigidbody.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}