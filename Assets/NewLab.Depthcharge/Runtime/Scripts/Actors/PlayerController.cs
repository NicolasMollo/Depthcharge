using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        private MovementModule _movementModule = null;
        public MovementModule MovementModule { get => _movementModule; }

        [SerializeField]
        private HealthModule _healthModule = null;
        public HealthModule HealthModule { get => _healthModule; }

        [SerializeField]
        private CollisionModule _collisionModule = null;
        public CollisionModule CollisionModule { get => _collisionModule; }


        private PlayerInputActions playerInput = null;

        private float horizontal = 0.0f;


        private void Start()
        {

            playerInput = new PlayerInputActions();
            _movementModule.SetUpModule(this.gameObject);

        }


        private void Update()
        {

            horizontal = playerInput.Std_ActionMap.HorizontalMovement.ReadValue<float>();
            _movementModule.MoveTarget(horizontal);

        }

    }

}