using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [DisallowMultipleComponent]
    public class MovementModule : BaseModule
    {

        [SerializeField]
        private BaseMovementAdapter target = null;

        [SerializeField]
        private BaseMovementStrategy movementStrategy = null;

        [SerializeField]
        [Range(1.0f, 50.0f)]
        private float movementSpeed = 1.0f;


        public override void SetUpModule(GameObject owner = null)
        {

            if (target == null)
            {
                Debug.LogError($"=== {this.name}.MovementModule.SetUpModule() === target is null!");
                return;
            }

            if (movementStrategy == null)
            {
                Debug.LogError($"=== {this.name}.MovementModule.SetUpModule() === movementStrategy is null!");
                return;
            }

        }

        public void MoveTarget(Vector2 direction = default)
        {

            movementStrategy.Movement(target, movementSpeed, direction);

        }

    }

}