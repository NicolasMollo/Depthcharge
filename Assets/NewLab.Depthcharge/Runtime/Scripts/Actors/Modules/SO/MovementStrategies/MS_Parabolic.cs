using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_Parabolic")]
    public class MS_Parabolic : BaseMovementStrategy
    {

        public override void Movement(MovementContext movementContext)
        {

            Vector2 force = new Vector2(0f, 0f);
            movementContext.Target.AddForce(force, ForceMode2D.Impulse);

        }

    }

}