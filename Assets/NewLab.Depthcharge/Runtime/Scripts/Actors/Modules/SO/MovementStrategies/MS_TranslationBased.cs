using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_TranslationBased")]
    public class MS_TranslationBased : BaseMovementStrategy
    {

        public override void Movement(BaseMovementAdapter target, float speed, Vector2 direction = default)
        {

            Vector2 directionNormalized = direction.normalized;
            float calculatedSpeed = speed * Time.deltaTime;
            Vector2 velocity = directionNormalized * calculatedSpeed;

            target.Translate(velocity);

        }

    }

}