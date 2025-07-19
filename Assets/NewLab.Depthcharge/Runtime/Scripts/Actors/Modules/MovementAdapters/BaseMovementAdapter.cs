using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseMovementAdapter : MonoBehaviour
    {

        public abstract void MoveTo(Vector2 position);
        public abstract Vector2 GetPosition();

        public virtual void Translate(Vector2 translation) { }
        public virtual void AddForce(Vector2 force, ForceMode2D forceMode2D) { }

    }

}