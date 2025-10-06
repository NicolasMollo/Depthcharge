using System;
using UnityEngine.InputSystem;

namespace Depthcharge.Actors.Modules
{

    public class InputModule : BaseModule
    {

        private PlayerInputActions playerInput = null;
        private float movementInput = 0.0f;

        private void Awake()
        {
            playerInput = new PlayerInputActions();
            playerInput.Std_ActionMap.Enable();
            IsModuleSetUp = true;
        }

        public override void EnableModule()
        {
            base.EnableModule();
            playerInput.Std_ActionMap.Enable();
        }
        public override void DisableModule()
        {
            base.DisableModule();
            playerInput.Std_ActionMap.Disable();
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