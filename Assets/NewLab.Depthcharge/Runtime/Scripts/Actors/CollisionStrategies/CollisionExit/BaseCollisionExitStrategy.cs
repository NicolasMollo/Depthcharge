using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    public abstract  class BaseCollisionExitStrategy : ScriptableObject
    {

        [SerializeField]
        protected string tag = string.Empty;

        public abstract void CollisionExit(Actor owner, Collider2D other);

    }
}