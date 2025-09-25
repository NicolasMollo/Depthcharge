using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UI_InputController : MonoBehaviour
    {

        private UIInputActions UIInput  = null;

        //private void Start()
        //{
        //    UIInput = new UIInputActions();
        //}
        //public void SetUp()
        //{
            
        //    UIInput.Std_ActionMap.Enable();
        //}

        //public void CleanUp()
        //{
        //    UIInput.Std_ActionMap.Disable();
        //}

        public void SetUp()
        {
            UIInput = new UIInputActions();
        }

        public void EnableInput()
        {
            UIInput.Std_ActionMap.Enable();
        }
        public void DisableInput()
        {
            UIInput.Std_ActionMap.Disable();
        }

        public void SubscribeOnUp(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Up.performed += method;
        }
        public void UnsubscribeFromUp(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Up.performed -= method;
        }

        public void SubscribeOnDown(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Down.performed += method;

        }
        public void UnsubscribeFromDown(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Down.performed -= method;
        }

        public void SubscribeOnConfirm(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Confirm.performed += method;
        }
        public void UnsubscribeFromConfirm(Action<InputAction.CallbackContext> method)
        {
            UIInput.Std_ActionMap.Confirm.performed -= method;
        }

    }

}