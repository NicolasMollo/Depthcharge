using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    public abstract class BaseCollisionStayStrategy : ScriptableObject
    {

        [SerializeField]
        protected string tag = string.Empty;

        public abstract void CollisionStay(Actor owner, Collider2D other);

    }
}