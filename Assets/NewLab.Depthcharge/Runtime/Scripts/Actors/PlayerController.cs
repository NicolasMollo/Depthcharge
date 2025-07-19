using UnityEngine;
using Depthcharge.Actors.Modules;
using UnityEngine.InputSystem;
using System;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour
    {

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


        private void Start()
        {

            _inputModule.SetUpModule();
            _movementModule.SetUpModule();
            _healthModule.SetUpModule();
            _shootModule.SetUpModule();
            _inputModule.SubscribeOnShoot(OnPressShootButton);

        }

        private void OnDestroy()
        {

            _inputModule.UnsubscribeFromShoot(OnPressShootButton);

        }

        private void OnPressShootButton(InputAction.CallbackContext context)
        {
            _shootModule.Shoot();
        }

        //[SerializeField]
        //private BaseBulletFactory bulletFactory = null;
        //private float counter = 20f;
        //private bool decreaseCounter = false;

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

    }

}