using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors
{

    public class PlayerController : Actor
    {

        #region Modules

        [Header("MODULES")]
        [SerializeField]
        private HealthModule _healthModule = null;
        [SerializeField]
        private MovementModule _movementModule = null;
        [SerializeField]
        private InputModule _inputModule = null;
        [SerializeField]
        private AnimationModule _animationModule = null;
        [SerializeField]
        private ShootModule _shootModule = null;

        #endregion

        public HealthModule HealthModule { get => _healthModule; }
        public MovementModule MovementModule { get => _movementModule; }
        public InputModule InputModule { get => _inputModule; }
        public AnimationModule AnimationModule { get => _animationModule; }
        public ShootModule ShootModule { get => _shootModule; }


        private void OnEnable()
        {
            _inputModule.SubscribeOnShoot(OnPressShootButton);
            _healthModule.OnTakeDamage += OnTakeDamage;
        }
        private void OnDisable()
        {
            _healthModule.OnTakeDamage -= OnTakeDamage;
            _inputModule.UnsubscribeFromShoot(OnPressShootButton);
        }
        private void Update()
        {
            float movementInput = _inputModule.GetMovementInput();
            Vector2 direction = new Vector2(movementInput, 0);
            _movementModule.MoveTarget(direction);
        }

        private void OnPressShootButton(InputAction.CallbackContext context)
        {
            _shootModule.Shoot();
        }
        private void OnTakeDamage(float health)
        {
            _animationModule.PlayAnimation(AnimationController.AnimationType.Damage);
        }

        public void EnableModules()
        {
            _inputModule.EnableModule();
            _movementModule.EnableModule();
            _healthModule.EnableModule();
            _shootModule.EnableModule();
        }
        public void DisableModules()
        {
            _inputModule.DisableModule();
            _movementModule.DisableModule();
            _healthModule.DisableModule();
            _shootModule.DisableModule();
        }

    }

}