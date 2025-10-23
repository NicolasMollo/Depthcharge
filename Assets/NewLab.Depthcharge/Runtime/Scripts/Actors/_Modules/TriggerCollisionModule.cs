using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class TriggerCollisionModule : BaseCollisionModule
    {

        public override void SetUpModule(GameObject owner = null)
        {
            base.SetUpModule(owner);

            if (!setComponentsAutomatically)
            {
                boxCollider = GetComponent<BoxCollider2D>();
            }
            boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            foreach (BaseCollisionStrategy strategy in collisionStrategies)
            {
                strategy.OnCollision(this.gameObject, collider);
            }
        }

    }

}