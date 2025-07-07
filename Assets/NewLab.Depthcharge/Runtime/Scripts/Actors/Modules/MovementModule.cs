using UnityEngine;

namespace Depthcharge.Actors
{
    public class MovementModule : BaseModule
    {

        private Transform targetTransform = null;

        [SerializeField]
        [Range(1.0f, 50.0f)]
        private float movementSpeed = 0.0f;

        public override void SetUpModule(GameObject owner = null)
        {

            if (owner == null)
            {
                Debug.LogError($"=== {this.name}.MovementModule.SetUpModule() === This module requires a GameObject owner to function!");
                return;
            }

            targetTransform = owner.transform;

        }

        public void MoveTarget(float directionX)
        {

            Vector2 direction = new Vector2(directionX, 0);
            Vector2 directionNormalized = direction.normalized;
            float calculatedMovementSpeed = movementSpeed * Time.deltaTime;
            Vector2 velocity = directionNormalized * calculatedMovementSpeed;

            targetTransform.Translate(velocity);

        }

    }

}