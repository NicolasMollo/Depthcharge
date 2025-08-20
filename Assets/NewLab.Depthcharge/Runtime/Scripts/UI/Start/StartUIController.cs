using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class StartUIController : MonoBehaviour
    {

        [SerializeField]
        private Image selector = null;
        [SerializeField]
        private List<UI_SceneButtonAdapter> sceneButtons = null;
        [SerializeField]
        private UI_StdButtonAdapter settingsButton = null;

        public void SetUp()
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.SetUp();
        }
        public void CleanUp()
        {
            foreach (UI_SceneButtonAdapter button in sceneButtons)
                button.CleanUp();
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