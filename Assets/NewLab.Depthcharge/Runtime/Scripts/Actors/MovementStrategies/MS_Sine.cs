using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_Sine")]
    public class MS_Sine : BaseMovementStrategy
    {

        [SerializeField]
        private float frequency = 2f;
        [SerializeField]
        private float amplitude = 0.5f;

        public override void MoveTarget(MovementContext movementContext)
        {
            float timeSinceSpawn = Time.time - movementContext.SpawnTime;
            Vector2 forwardMovement = Vector2.up * movementContext.Speed;
            float sinOffset = Mathf.Sin(timeSinceSpawn * frequency) * amplitude;
            Vector2 lateralMovement = Vector2.right * sinOffset;
            Vector3 velocity = forwardMovement + lateralMovement;

            movementContext.Target.SetVelocity(velocity);
        }

    }

}