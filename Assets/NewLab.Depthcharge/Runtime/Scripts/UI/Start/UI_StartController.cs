using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UI_StartController : MonoBehaviour
    {

        [SerializeField]
        private UI_InputController input = null;
        [SerializeField]
        private UI_SelectionController selection = null;
        public UI_SelectionController Selection { get => selection; }
        [SerializeField]
        private UI_Selector selector = null;
        [SerializeField]
        private List<BaseButtonAdapter> buttons = null;


        private void Start()
        {
            SelectionContext selectionContext = new SelectionContext(buttons, input, selector);
            selection.SetUp(selectionContext);
        }

        public void ResetSelection()
        {
            selection.ResetSelection();
        }

        public void SubscribeToSceneButtons(Action<SceneConfiguration> method)
        {
            UI_SceneButtonAdapter sceneButton = null;
            foreach (BaseButtonAdapter button in buttons)
            {
                sceneButton = button as UI_SceneButtonAdapter;
                if (sceneButton != null)
                    sceneButton.OnClick += method;
            }
        }
        public void UnsubscribeFromSceneButtons(Action<SceneConfiguration> method)
        {
            UI_SceneButtonAdapter sceneButton = null;
            foreach (BaseButtonAdapter button in buttons)
            {
                sceneButton = button as UI_SceneButtonAdapter;
                if (sceneButton != null)
                    sceneButton.OnClick -= method;
            }
        }
        public void SubscribeToExitButton(Action<int> method)
        {
            UI_StdButtonAdapter exitButton = null;
            foreach (BaseButtonAdapter button in buttons)
            {
                exitButton = button as UI_StdButtonAdapter;
                if (exitButton != null)
                {
                    exitButton.OnClick += method;
                    break;
                }
            }
        }
        public void UnsubscribeFromExitButton(Action<int> method)
        {
            UI_StdButtonAdapter exitButton = null;
            foreach (BaseButtonAdapter button in buttons)
            {
                exitButton = button as UI_StdButtonAdapter;
                if (exitButton != null)
                {
                    exitButton.OnClick -= method;
                    break;
                }
            }
        }

        public void EnableInput()
        {
            input.EnableInput();
            selection.SubscribeToInput();
        }
        public void DisableInput()
        {
            selection.UnsubscribeFromInput();
            input.DisableInput();
        }

    }

}