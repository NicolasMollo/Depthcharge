using UnityEngine;

namespace Depthcharge.Actors.Modules
{

    [DisallowMultipleComponent]
    public class MovementModule : BaseModule
    {

        [SerializeField]
        private BaseMovementAdapter _target = null;
        public BaseMovementAdapter Target { get => _target; }
        [SerializeField]
        private BaseMovementStrategy movementStrategy = null;

        [Header("SETTINGS")]
        [SerializeField]
        private MovementContext movementContext = default;
        public float MovementSpeed { get => movementContext.Speed; }

        private void Awake()
        {
            if (_target == null)
            {
                Debug.LogError($"=== {this.name}.MovementModule.SetUpModule() === target is null!");
                return;
            }
            if (movementStrategy == null)
            {
                Debug.LogError($"=== {this.name}.MovementModule.SetUpModule() === movementStrategy is null!");
                return;
            }
            IsModuleSetUp = true;
        }

        public void MoveTarget(Vector2 direction = default)
        {
            movementContext.Direction = direction;
            movementStrategy.MoveTarget(movementContext);
        }
        public void SetMovementSpeed(float speed)
        {
            movementContext.Speed = speed;
        }
        public void SetMovementStrategy(BaseMovementStrategy movementStrategy)
        {
            this.movementStrategy = movementStrategy;
        }

    }

}