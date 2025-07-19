using UnityEngine;

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

    }

}