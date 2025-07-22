using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_TranslationBased")]
    public class MS_TranslationBased : BaseMovementStrategy
    {

        public override void Movement(MovementContext movementContext)
        {

            Vector2 directionNormalized = movementContext.Direction.normalized;
            float calculatedSpeed = movementContext.Speed * Time.deltaTime;
            Vector2 velocity = directionNormalized * calculatedSpeed;

            movementContext.Target.Translate(velocity);

        }

    }

}