using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/MovementStrategies/MS_Parabolic")]
    public class MS_Parabolic : BaseMovementStrategy
    {

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float forceY = 0.0f;

        public override void MoveTarget(MovementContext movementContext)
        {
            Vector2 force = new Vector2(0f, forceY);
            movementContext.Target.AddForce(force, ForceMode2D.Impulse);
        }

    }

}