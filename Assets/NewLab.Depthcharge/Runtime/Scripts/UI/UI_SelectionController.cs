using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UI_SelectionController : MonoBehaviour
    {

        private UI_InputController input = null;
        private List<BaseButtonAdapter> buttons = null;
        private UI_Selector selector = null;
        private BaseButtonAdapter selectedButton = null;

        public void SetUp(SelectionContext context)
        {
            buttons = context.buttons;
            foreach (BaseButtonAdapter button in buttons)
            {
                button.SetUp();
                button.OnHover += OnButtonHover;
            }
            selector = context.selector;
            SetSelectedButton(buttons[0]);
            input = context.input;
            input.SubscribeOnUp(OnInputUp);
            input.SubscribeOnDown(OnInputDown);
            input.SubscribeOnConfirm(OnInputConfirm);
        }
        public void CleanUp()
        {
            input.UnsubscribeFromConfirm(OnInputConfirm);
            input.UnsubscribeFromDown(OnInputDown);
            input.UnsubscribeFromUp(OnInputUp);
            foreach (BaseButtonAdapter button in buttons)
            {
                button.OnHover -= OnButtonHover;
                button.CleanUp();
            }
        }

        private void OnButtonHover(BaseButtonAdapter button)
        {
            BaseButtonAdapter previousButton = selectedButton;
            if (button != previousButton)
                SetSelectedButton(button);
        }
        private void OnInputUp(InputAction.CallbackContext context)
        {
            int index = buttons.IndexOf(selectedButton);
            index--;
            if (index < 0)
                index = buttons.Count - 1;
            SetSelectedButton(buttons[index]);
        }
        private void OnInputDown(InputAction.CallbackContext context)
        {
            int index = buttons.IndexOf(selectedButton);
            index++;
            if (index == buttons.Count)
                index = 0;
            SetSelectedButton(buttons[index]);
        }
        private void OnInputConfirm(InputAction.CallbackContext context)
        {
            selectedButton.OnButtonClick();
        }

        private void SetSelectedButton(BaseButtonAdapter button)
        {
            selectedButton = button;
            Debug.Log($"=== {this.name} === selected button is: {selectedButton.name}");
            selector.SetSelectorPosition(selectedButton.Image.rectTransform.position.y);
        }

    }

}