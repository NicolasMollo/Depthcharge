using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_VelocityBased")]
    public class MS_VelocityBased : BaseMovementStrategy
    {
        public override void MoveTarget(MovementContext movementContext)
        {
            Vector2 directionNormalized = movementContext.Direction.normalized;
            float calculatedSpeed = movementContext.Speed;
            Vector2 velocity = directionNormalized * calculatedSpeed;

            movementContext.Target.SetVelocity(velocity);
        }
    }

}