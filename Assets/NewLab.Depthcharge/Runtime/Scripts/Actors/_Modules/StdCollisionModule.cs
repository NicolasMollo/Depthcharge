using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class StdCollisionModule : BaseCollisionModule
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (BaseCollisionStrategy strategy in collisionStrategies)
            {
                LastCollisionLayer = collision.gameObject.layer;
                strategy.OnCollision(this.gameObject, collision.collider);
            }
        }

    }

}