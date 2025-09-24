using Depthcharge.SceneManagement;
using Depthcharge.Toolkit;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class LoseUIController : MonoBehaviour
    {

        [SerializeField]
        private UI_PostGameMenu menu = null;
        [SerializeField]
        private SceneConfiguration campaignSceneConfig = null;
        [SerializeField]
        private SceneConfiguration survivalSceneConfig = null;
        private SceneConfiguration selectedSceneConfig = null;
        private SelectionContext selectionContext = default;
        [SerializeField]
        private UI_InputController input = null;
        [SerializeField]
        private UI_SelectionController selection = null;
        [SerializeField]
        private UI_Selector selector = null;
        [Header("PANEL SETTINGS")]
        [SerializeField]
        private UI_FadeableAdapter fadeablePanel = null;
        [SerializeField]
        private float fadeInDelay = 0.0f;
        [SerializeField]
        private float fadeInTreshold = 0.0f;
        [SerializeField]
        private float fadeOutDelay = 0.0f;
        public bool isPanelFadedIn { get => fadeablePanel.IsFadedIn; }

        public void SetUp()
        {
            menu.SetUp();
            input.SetUp();
            selectionContext = new SelectionContext(menu.Buttons, input, selector);
            selection.SetUp(selectionContext);
        }
        public void CleanUp()
        {
            selection.CleanUp();
        }

        public void AddListeners(StartUIController startUI)
        {
            startUI.SubscribeToSceneButtons(OnClickSceneButtons);
        }
        public void RemoveListeners(StartUIController startUI)
        {
            startUI.UnsubscribeFromSceneButtons(OnClickSceneButtons);
        }

        private void OnClickSceneButtons(SceneConfiguration configuration)
        {
            if (configuration.SceneName == survivalSceneConfig.SceneName)
                menu.SetReloadButtonArg(survivalSceneConfig);
            else
                menu.SetReloadButtonArg(campaignSceneConfig);
            selectedSceneConfig = configuration;
        }

        public void ActivateMenu()
        {
            if (selectedSceneConfig.name == survivalSceneConfig.name)
                menu.ActivateTimeBlock();
            else
                menu.DeactivateTimeBlock();
            menu.gameObject.SetActive(true);
        }
        public void DeactivateMenu()
        {
            menu.gameObject.SetActive(false);
        }

        public void FadeInPanel()
        {
            fadeablePanel.FadeIn(fadeInDelay, fadeInTreshold);
        }

        public void FadeOutPanel()
        {
            fadeablePanel.FadeOut(fadeOutDelay);
        }

    }

}