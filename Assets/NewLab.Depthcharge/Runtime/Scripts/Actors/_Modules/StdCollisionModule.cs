using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class StdCollisionModule : BaseCollisionModule
    {

        public override void SetUpModule(GameObject owner = null)
        {
            base.SetUpModule(owner);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (BaseCollisionStrategy strategy in collisionStrategies)
            {
                strategy.OnCollision(this.gameObject, collision.collider);
            }
        }

    }

}