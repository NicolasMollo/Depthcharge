using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    public abstract class BaseMovementStrategy : ScriptableObject
    {

        public abstract void Movement(BaseMovementAdapter target, float speed, Vector2 direction = default);

    }

}