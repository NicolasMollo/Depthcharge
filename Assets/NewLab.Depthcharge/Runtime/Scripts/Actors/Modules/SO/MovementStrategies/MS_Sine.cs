using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_Sine")]
    public class MS_Sine : BaseMovementStrategy
    {

        [SerializeField]
        private Vector2 forwardDirection = Vector2.zero;

        [SerializeField]
        private Vector2 lateralDirection = Vector2.zero;

        [SerializeField]
        private float frequency = 2f;

        [SerializeField]
        private float amplitude = 0.5f;

        public override void Movement(MovementContext movementContext)
        {

            Vector2 forwardDirection = this.forwardDirection;
            Vector2 lateralDirection = this.lateralDirection;
            float phaseOffset = (movementContext.Target.GetInstanceID() % 360) * Mathf.Deg2Rad;
            float wave = Mathf.Sin(Time.time * frequency);
            Vector2 velocity = forwardDirection * movementContext.Speed;

            movementContext.Target.SetVelocity(velocity);

        }

    }


}