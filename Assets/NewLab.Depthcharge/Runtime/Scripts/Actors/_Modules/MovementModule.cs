using UnityEngine;
using UnityEngine.Assertions;

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
            string message = $"=== {this.name}.MovementModule.SetUpModule() === Be ensure to fill \"Owner\" field in Inspector!";
            Assert.IsNotNull(owner, message);
            message = $"=== {owner.name}.MovementModule.SetUpModule() === target is null!";
            Assert.IsNotNull(_target, message);
            message = $"=== {owner.name}.MovementModule.SetUpModule() === movementStrategy is null!";
            Assert.IsNotNull(movementStrategy, message);
        }

        public void MoveTarget(Vector2 direction = default, Transform targetToReach = null)
        {
            movementContext.Direction = direction;
            movementContext.TargetToReach = targetToReach;
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

        public override void EnableModule()
        {
            _target.EnableMovement();
        }
        public override void DisableModule()
        {
            _target.DisableMovement();
        }

    }

}