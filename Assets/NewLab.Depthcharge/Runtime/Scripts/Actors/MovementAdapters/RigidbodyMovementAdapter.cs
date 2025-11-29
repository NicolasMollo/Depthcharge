using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.Modules
{

    public class RigidbodyMovementAdapter : BaseMovementAdapter
    {

        private RigidbodyConstraints2D startConstraint = default;
        private float startGravity = 0.0f;
        private float startDrag = 0.0f;
        [SerializeField]
        private Rigidbody2D rb = null;

        private void Awake()
        {
            string message = $"=== RigidbodyMovementAdapter.Awake() === \"Rigidbody2D\" component is required!";
            Assert.IsNotNull(rb, message);
            startConstraint = rb.constraints;
            startGravity = rb.gravityScale;
            startDrag = rb.drag;
        }

        public override Vector2 GetPosition()
        {
            return rb.position;
        }

        public override void MoveTo(Vector2 position)
        {
            rb.MovePosition(position);
        }

        public override void AddForce(Vector2 force, ForceMode2D forceMode2D)
        {
            rb.AddForce(force, forceMode2D);
        }

        public override void SetVelocity(Vector2 velocity)
        {
            rb.velocity = velocity;
        }

        public override void Rotate(float rotation, float speedRotation)
        {
            float smoothRotation = Mathf.MoveTowardsAngle(rb.rotation, rotation, speedRotation * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
        public override void SetRotation(float angle)
        {
            rb.SetRotation(angle);
        }

        public override void EnableMovement()
        {
            rb.constraints = startConstraint;
        }
        public override void DisableMovement()
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }

        public override void SetGravity(float gravity)
        {
            rb.gravityScale = gravity;
        }
        public override void ResetGravity()
        {
            rb.gravityScale = startGravity;
        }

        public override void SetDrag(float drag)
        {
            rb.drag = drag;
        }
        public override void ResetDrag()
        {
            rb.drag = startDrag;
        }

    }

}