using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseMovementStrategy : ScriptableObject
    {
        public abstract void Movement(MovementContext movementContext);
    }

}