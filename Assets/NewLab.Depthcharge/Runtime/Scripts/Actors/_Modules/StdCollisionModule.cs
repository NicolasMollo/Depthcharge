using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class StdCollisionModule : BaseCollisionModule
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (BaseCollisionStrategy strategy in collisionStrategies)
            {
                strategy.OnCollision(this.gameObject, collision.collider);
                LastCollisionLayer = collision.gameObject.layer;
            }
        }

    }

}