using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class StdCollisionModule : BaseCollisionModule
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            LastCollisionLayer = collision.gameObject.layer; 
            foreach (BaseCollisionEnterStrategy strategy in collisionStrategies)
            {
                strategy.CollisionEnter(owner, collision.collider);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            foreach (BaseCollisionStayStrategy strategy in stayStrategies)
            {
                // LastCollisionLayer = collision.gameObject.layer;
                strategy.CollisionStay(owner, collision.collider);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            foreach (BaseCollisionExitStrategy strategy in exitStrategies)
            {
                // LastCollisionLayer = collision.gameObject.layer;
                strategy.CollisionExit(owner, collision.collider);
            }
        }

    }

}