using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {

        #region Modules

        [SerializeField]
        private InputModule _inputModule = null;
        public InputModule InputModule { get => _inputModule; }

        [SerializeField]
        private MovementModule _movementModule = null;
        public MovementModule MovementModule { get => _movementModule; }

        [SerializeField]
        private HealthModule _healthModule = null;
        public HealthModule HealthModule { get => _healthModule; }

        [SerializeField]
        private ShootModule _shootModule = null;
        public ShootModule ShootModule { get => _shootModule; }

        #endregion

        private void OnEnable()
        {
            _inputModule.SubscribeOnShoot(OnPressShootButton);
        }
        private void OnDisable()
        {
            _inputModule.UnsubscribeFromShoot(OnPressShootButton);
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

        private void Update()
        {
            float movementInput = _inputModule.GetMovementInput();
            Vector2 direction = new Vector2(movementInput, 0);
            _movementModule.MoveTarget(direction);

            #region Debug

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    _shootModule.Shoot();
            //}
            //else if (Input.GetKeyDown(KeyCode.R))
            //{
            //    _shootModule.Reload();
            //}
            //else if (Input.GetKeyDown(KeyCode.F))
            //{
            //    _shootModule.ChangeBulletsType(bulletFactory);
            //    counter = 20f;
            //    decreaseCounter = true;
            //}

            //if (decreaseCounter)
            //{
            //    counter -= Time.deltaTime;
            //    if (counter <= 0)
            //    {
            //        _shootModule.ResetBulletsType();
            //        decreaseCounter = !decreaseCounter;
            //    }
            //    Debug.Log(counter);
            //}

            #endregion
        }

        private void OnPressShootButton(InputAction.CallbackContext context)
        {
            _shootModule.Shoot();
        }

    }

}