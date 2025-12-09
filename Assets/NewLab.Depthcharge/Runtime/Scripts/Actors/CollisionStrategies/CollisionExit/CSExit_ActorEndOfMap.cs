using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/CollisionStrategies/Exit/CSExit_ActorEndOfMap")]
    public class CSExit_ActorEndOfMap : BaseCollisionExitStrategy
    {

        public override void CollisionExit(Actor owner, Collider2D other)
        {
            if (other.CompareTag(tag))
            {
                if (owner == null) return;
                EndOfMapContext context = CollisionStrategyHelper.CreateEndOfMapContext(other);
                owner.OnCollisionExitWithEndOfMap(context);
            }
        }

    }
}