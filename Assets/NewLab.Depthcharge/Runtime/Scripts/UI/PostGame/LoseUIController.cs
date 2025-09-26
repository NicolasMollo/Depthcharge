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
        public UI_PostGameMenu Menu { get => menu; }
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

        private void Start()
        {
            // menu.SetUp();
            selectionContext = new SelectionContext(menu.Buttons, input, selector);
            selection.SetUp(selectionContext);
        }
        //public void SetUp()
        //{
        //    menu.SetUp();
        //    input.SetUp();
        //    selectionContext = new SelectionContext(menu.Buttons, input, selector);
        //    selection.SetUp(selectionContext);
        //}
        //private void OnDisable()
        //{
        //    selection.CleanUp();
        //}

        public void ResetSelection()
        {
            selection.ResetSelection();
        }
        //public void CleanUp()
        //{
        //    selection.CleanUp();
        //}

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

        public void SetDefeatedText(string text)
        {
            menu.SetDefeatedText(text);
        }
        public void SetMissedText(string text)
        {
            menu.SetMissedText(text);
        }
        public void SetScoreText(string text)
        {
            menu.SetScoreText(text);
        }
        public void SetTimeText(string text)
        {
            if (selectedSceneConfig.name != survivalSceneConfig.name)
                return;
            menu.SetTimeText(text);
        }

        public void SetSelectorActiveness(bool activeness)
        {
            selector.gameObject.SetActive(activeness);
        }
        public void SetMenuActiveness(bool activeness)
        {
            menu.gameObject.SetActive(activeness);
        }
        public void SetDefeatedBlockActiveness(bool activeness)
        {
            menu.SetDefeatedBlockActiveness(activeness);
        }
        public void SetMissedBlockActiveness(bool activeness)
        {
            menu.SetMissedBlockActiveness(activeness);
        }
        public void SetScoreBlockActiveness(bool activeness)
        {
            menu.SetScoreBlockActiveness(activeness);
        }
        public void SetTimeBlockActiveness(bool activeness)
        {
            if (selectedSceneConfig.name != survivalSceneConfig.name)
            {
                menu.SetTimeBlockActiveness(false);
                return;
            }
            menu.SetTimeBlockActiveness(activeness);
        }

        public void FadeInPanel()
        {
            fadeablePanel.FadeIn(fadeInDelay, fadeInTreshold);
        }
        public void FadeOutPanel()
        {
            fadeablePanel.FadeOut(fadeOutDelay);
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