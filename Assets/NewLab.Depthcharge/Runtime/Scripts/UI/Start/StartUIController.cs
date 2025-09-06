using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class StartUIController : MonoBehaviour
    {

        private List<BaseButtonAdapter> buttons = null;
        private BaseButtonAdapter selectedButton = null;

        [SerializeField]
        private UI_InputController input = null;
        [SerializeField]
        private UI_Selector selector = null;
        [SerializeField]
        private UI_StdButtonAdapter settingsButton = null;
        [SerializeField]
        private List<UI_SceneButtonAdapter> sceneButtons = null;

        public void SetUp()
        {
            buttons = new List<BaseButtonAdapter>();
            buttons.AddRange(sceneButtons);
            buttons.Add(settingsButton);
            foreach (BaseButtonAdapter button in buttons)
            {
                button.SetUp();
                button.OnHover += OnButtonHover;
            }
            SetSelectedButton(buttons[0]);
            input.SetUp();
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

        public void SubscribeToSceneButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.OnClick += method;
        }
        public void UnsubscribeToSceneButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.OnClick -= method;
        }
        public void SubscribeToSettingsButton(Action<int> method)
        {
            settingsButton.OnClick += method;
        }
        public void UnsubscribeToSettingsButton(Action<int> method)
        {
            settingsButton.OnClick -= method;
        }

    }

}