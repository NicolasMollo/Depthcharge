using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors.Modules
{

    public class InputModule : BaseModule
    {

        private PlayerInputActions _playerInput = null;
        private float movementInput = 0.0f;

        public override void SetUpModule(GameObject owner = null)
        {
            _playerInput = new PlayerInputActions();
            _playerInput.Std_ActionMap.Enable();
        }

        public float GetMovementInput()
        {
            movementInput = _playerInput.Std_ActionMap.HorizontalMovement.ReadValue<float>();
            return movementInput;
        }

        public void SubscribeOnShoot(Action<InputAction.CallbackContext> method)
        {
            _playerInput.Std_ActionMap.Shoot.performed += method;
        }
        public void UnsubscribeFromShoot(Action<InputAction.CallbackContext> method)
        {
            _playerInput.Std_ActionMap.Shoot.performed -= method;
        }


    }

}