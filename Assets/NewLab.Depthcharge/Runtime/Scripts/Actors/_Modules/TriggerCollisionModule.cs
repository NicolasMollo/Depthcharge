using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public class TriggerCollisionModule : BaseCollisionModule
    {

        public override void SetUpModule()
        {
            base.SetUpModule();
            boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            LastCollisionLayer = collider.gameObject.layer;
            foreach (BaseCollisionEnterStrategy strategy in collisionStrategies)
            {
                strategy.CollisionEnter(owner, collider);
            }
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            foreach (BaseCollisionStayStrategy strategy in stayStrategies)
            {
                // LastCollisionLayer = collider.gameObject.layer;
                strategy.CollisionStay(owner, collider);
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            foreach (BaseCollisionExitStrategy strategy in exitStrategies)
            {
                // LastCollisionLayer = collider.gameObject.layer;
                strategy.CollisionExit(owner, collider);
            }
        }

    }

}