using Depthcharge.Toolkit;
using UnityEngine;

namespace Depthcharge.UI
{
    [DisallowMultipleComponent]
    public class WinUIController : MonoBehaviour
    {

        [SerializeField]
        private UI_PostGameMenu menu;
        public UI_PostGameMenu Menu { get => menu; }
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
            SelectionContext selectionContext = new SelectionContext(menu.Buttons, input, selector);
            selection.SetUp(selectionContext);
        }

        public void ResetSelection()
        {
            selection.ResetSelection();
        }
        public void SetSelectorActiveness(bool activeness)
        {
            selector.gameObject.SetActive(activeness);
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