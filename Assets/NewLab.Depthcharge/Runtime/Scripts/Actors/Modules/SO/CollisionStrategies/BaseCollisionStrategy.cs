using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseCollisionStrategy : ScriptableObject
    {

        public abstract void OnCollision(GameObject owner, Collider2D other);

    }

}