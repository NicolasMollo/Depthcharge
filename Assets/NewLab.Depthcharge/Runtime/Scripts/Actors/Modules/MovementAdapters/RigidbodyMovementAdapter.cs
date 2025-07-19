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

    }

}