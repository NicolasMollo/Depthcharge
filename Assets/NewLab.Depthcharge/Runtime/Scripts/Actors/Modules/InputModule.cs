using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors.Modules
{

    public class InputModule : BaseModule
    {

        private PlayerInputActions playerInput = null;
        private float movementInput = 0.0f;

        public override void SetUpModule(GameObject owner = null)
        {
            playerInput = new PlayerInputActions();
            playerInput.Std_ActionMap.Enable();
        }

        public float GetMovementInput()
        {
            movementInput = playerInput.Std_ActionMap.HorizontalMovement.ReadValue<float>();
            return movementInput;
        }

        public void SubscribeOnShoot(Action<InputAction.CallbackContext> method)
        {
            playerInput.Std_ActionMap.Shoot.performed += method;
        }
        public void UnsubscribeFromShoot(Action<InputAction.CallbackContext> method)
        {
            playerInput.Std_ActionMap.Shoot.performed -= method;
        }

    }

}