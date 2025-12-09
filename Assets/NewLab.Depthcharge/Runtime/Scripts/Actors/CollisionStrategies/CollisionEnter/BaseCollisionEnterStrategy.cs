using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    public abstract class BaseCollisionEnterStrategy : ScriptableObject
    {

        [SerializeField]
        protected string tag = string.Empty;

        public abstract void CollisionEnter(Actor owner, Collider2D other);

    }
}