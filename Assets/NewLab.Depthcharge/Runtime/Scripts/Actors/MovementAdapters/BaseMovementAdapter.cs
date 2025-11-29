using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseMovementAdapter : MonoBehaviour
    {

        public abstract void MoveTo(Vector2 position);
        public abstract Vector2 GetPosition();

        public virtual void Translate(Vector2 translation) { }
        public virtual void AddForce(Vector2 force, ForceMode2D forceMode2D) { }
        public virtual void SetVelocity(Vector2 velocity) { }

        public virtual void Rotate(float rotation, float speedRotation) { }
        public virtual void SetRotation(float angle) { }

        public virtual void EnableMovement() { }
        public virtual void DisableMovement() { }

        public virtual void SetGravity(float gravity) { }
        public virtual void ResetGravity() { }
        public virtual void SetDrag(float drag) { }
        public virtual void ResetDrag() { }

    }

}