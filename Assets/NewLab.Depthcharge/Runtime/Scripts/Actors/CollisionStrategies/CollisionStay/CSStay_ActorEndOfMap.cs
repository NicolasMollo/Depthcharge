using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/Stay/CSStay_ActorAndEndOfMap")]
    public class CSStay_ActorEndOfMap : BaseCollisionStayStrategy
    {

        public override void CollisionStay(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                if (owner == null) return;
                EndOfMapContext context = CollisionStrategyHelper.CreateEndOfMapContext(other);
                owner.OnCollisionStayWithEndOfMap(context);
            }
        }

    }
}