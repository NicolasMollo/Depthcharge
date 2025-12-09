using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/Enter/CSEnter_ActorAndEndOfMap")]
    public class CSEnter_ActorEndOfMap : BaseCollisionEnterStrategy
    {

        public override void CollisionEnter(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                if (owner == null) return;
                EndOfMapContext context = CollisionStrategyHelper.CreateEndOfMapContext(other);
                owner.OnCollisionWithEndOfMap(context);
            }
        }

    }
}