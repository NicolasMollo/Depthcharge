using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseCollisionStrategy : ScriptableObject
    {
        public abstract void OnCollision(Actor owner, Collider2D other);
    }

}