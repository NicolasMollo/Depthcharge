using Depthcharge.Toolkit;
using UnityEngine;

namespace Depthcharge.UI
{
    [DisallowMultipleComponent]
    public class WinUIController : MonoBehaviour
    {

        [SerializeField]
        private UI_PostGameMenu menu;
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

        public void ActivateMenu()
        {
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