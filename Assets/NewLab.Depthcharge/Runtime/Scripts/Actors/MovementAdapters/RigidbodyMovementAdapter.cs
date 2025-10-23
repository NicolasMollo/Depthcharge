using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class RigidbodyMovementAdapter : BaseMovementAdapter
    {

        [SerializeField]
        private Rigidbody2D rb = null;

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

    }

}