using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class StartUIController : MonoBehaviour
    {

        private List<BaseButtonAdapter> buttons = null;
        private SelectionContext selectionContext = default;
        [SerializeField]
        private UI_InputController input = null;
        [SerializeField]
        private UI_SelectionController selection = null;
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
            input.SetUp();
            selectionContext = new SelectionContext(buttons, input, selector);
            selection.SetUp(selectionContext);
        }
        public void CleanUp()
        {
            selection.CleanUp();
        }
        public void SubscribeToSceneButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.OnClick += method;
        }
        public void UnsubscribeFromSceneButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.OnClick -= method;
        }
        public void SubscribeToSettingsButton(Action<int> method)
        {
            settingsButton.OnClick += method;
        }
        public void UnsubscribeFromSettingsButton(Action<int> method)
        {
            settingsButton.OnClick -= method;
        }

        public void EnableInput()
        {
            input.EnableInput();
        }
        public void DisableInput()
        {
            input.DisableInput();
        }

    }

}