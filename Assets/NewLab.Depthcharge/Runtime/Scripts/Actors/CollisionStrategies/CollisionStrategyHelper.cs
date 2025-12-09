using UnityEngine;

namespace Depthcharge.Actors.Modules
{
    internal static class CollisionStrategyHelper
    {

        internal static EndOfMapContext CreateEndOfMapContext(Collider2D other)
        {
            EndOfMapContext context = new EndOfMapContext();
            context.tag = other.tag;
            context.layer = other.gameObject.layer;

            return context;
        }

    }
}