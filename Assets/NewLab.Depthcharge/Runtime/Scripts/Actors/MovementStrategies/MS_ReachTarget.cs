using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_ReachTarget")]
    public class MS_ReachTarget : BaseMovementStrategy
    {

        [SerializeField]
        private bool rotation = true;
        [SerializeField]
        private float rotationOffset = 0.0f;
        [SerializeField]
        [Tooltip("Of how degrees per seconds can rotate")]
        [Range(0.0f, 360.0f)]
        private float speedRotation = 0.0f;

        public override void MoveTarget(MovementContext movementContext)
        {
            Vector2 direction = ((Vector2)movementContext.TargetToReach.position - movementContext.Target.GetPosition()).normalized;
            float calculatedSpeed = movementContext.Speed;
            Vector2 velocity = direction * calculatedSpeed;
            movementContext.Target.SetVelocity(velocity);
            if (rotation)
            {
                RotateTarget(movementContext);
            }
        }

        private void RotateTarget(MovementContext movementContext)
        {
            Vector2 direction = ((Vector2)movementContext.TargetToReach.position - movementContext.Target.GetPosition());
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationOffset;
            movementContext.Target.Rotate(targetAngle, speedRotation);
        }

    }
}