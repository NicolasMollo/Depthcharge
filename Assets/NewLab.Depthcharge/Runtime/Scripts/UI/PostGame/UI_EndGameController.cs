using Depthcharge.SceneManagement;
using Depthcharge.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.UI.EndGame
{

    [DisallowMultipleComponent]
    public class UI_EndGameController : MonoBehaviour
    {

        public bool AreMenuTextsConfigured { get => menu.AreTextsConfigured; }
        public bool IsPanelFadedIn { get => fadeablePanel.IsFadedIn; }
        public bool LastLevelPanelFadedIn { get => endGamePanel.IsFadedIn; }
        public bool IsEndGameTextConfigured { get => endGameText.IsSet; }

        #region UI elements

        [Header("UI ELEMENTS")]
        [SerializeField]
        private UI_EndGameMenu menu = null;
        [SerializeField]
        private UI_InputController input = null;
        [SerializeField]
        private UI_SelectionController selection = null;
        [SerializeField]
        private UI_Selector selector = null;
        [SerializeField]
        private UI_FadeableAdapter fadeablePanel = null;
        [SerializeField]
        private UI_FadeableAdapter endGamePanel = null;
        [SerializeField]
        private UI_EndGameText endGameText = null;
        [SerializeField]
        private string endGameTextToSet = string.Empty;

        #endregion
        #region Panel settings

        [Header("PANELS SETTINGS")]
        [SerializeField]
        private float fadeInDelay = 0.0f;
        [SerializeField]
        private float fadeInTreshold = 0.0f;
        [SerializeField]
        private float fadeOutDelay = 0.0f;

        [SerializeField]
        private float endGamePanelFadeInDelay = 0.0f;
        [SerializeField]
        private float endGamePanelFadeInTreshold = 0.0f;
        [SerializeField]
        private float endGamePanelFadeOutDelay = 0.0f;

        #endregion

        private void Start()
        {
            List<BaseButtonAdapter> buttonAdapters = new List<BaseButtonAdapter>();
            foreach (UI_EndGameButton endGameButton in menu.Buttons)
                buttonAdapters.Add(endGameButton.Button);
            SelectionContext selectionContext = new SelectionContext(buttonAdapters, input, selector);
            selection.SetUp(selectionContext);
            SetEndGameTextActiveness(false);
        }

        #region Menu

        public void SetMenuActiveness(bool activeness)
        {
            menu.gameObject.SetActive(activeness);
        }
        public void SetBlocksActiveness(bool activeness)
        {
            menu.SetAllBlocksActiveness(activeness);
        }
        public void SetButtonsActiveness(bool activeness)
        {
            menu.SetAllButtonsActiveness(activeness);
        }
        public void ConfigureTexts(EndGameMenuTexts texts, bool configureTimeText = true)
        {
            StartCoroutine(ConfigureMenuTexts(texts, configureTimeText));
        }
        private IEnumerator ConfigureMenuTexts(EndGameMenuTexts texts, bool configureTimeText = true)
        {
            bool blocksActiveness = true;
            foreach (UI_EndGameText text in menu.Texts)
            {
                if (!configureTimeText && text.Type == EndGameTextType.Time)
                {
                    text.IsSet = true;
                    continue;
                }
                menu.SetBlockActiveness(text.Type, blocksActiveness);
                menu.SetText(text.Type, GetTextFromEndGameTexts(text.Type, texts));
                yield return new WaitUntil(() => text.IsSet);
            }
        }
        private string GetTextFromEndGameTexts(EndGameTextType type, EndGameMenuTexts endGameTexts)
        {
            string text = string.Empty;
            switch (type)
            {
                case EndGameTextType.Defeated:
                    text = endGameTexts.enemiesDefeatedText;
                    break;
                case EndGameTextType.Missed:
                    text = endGameTexts.enemiesMissedText;
                    break;
                case EndGameTextType.Score:
                    text = endGameTexts.scoreText;
                    break;
                case EndGameTextType.Time:
                    text = endGameTexts.timeText;
                    break;
            }
            return text;
        }

        public void SetAllTextsState(bool enablingState)
        {
            menu.SetAllTextsState(enablingState);
        }

        public void SubscribeToButton(EndGameButtonType type, Action<SceneConfiguration> method)
        {
            menu.SubscribeToButton(type, method);
        }
        public void UnsubscribeFromButton(EndGameButtonType type, Action<SceneConfiguration> method)
        {
            menu.UnsubscribeFromButton(type, method);
        }
        public void SubscribeToButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_EndGameButton endGameButton in menu.Buttons)
                endGameButton.Button.OnClick += method;
        }
        public void UnsubscribeFromButtons(Action<SceneConfiguration> method)
        {
            foreach (UI_EndGameButton endGameButton in menu.Buttons)
                endGameButton.Button.OnClick -= method;
        }
        public void SetButtonArg(EndGameButtonType type, SceneConfiguration arg)
        {
            menu.SetButtonArg(type, arg);
        }

        #endregion
        #region Selection

        public void ResetSelection()
        {
            selection.ResetSelection();
        }
        public void SetSelectorActiveness(bool activeness)
        {
            selector.gameObject.SetActive(activeness);
        }

        #endregion
        #region Input

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

        #endregion
        #region Panel

        public void FadeInPanel()
        {
            fadeablePanel.FadeIn(fadeInDelay, fadeInTreshold);
        }
        public void FadeOutPanel()
        {
            fadeablePanel.FadeOut(fadeOutDelay);
        }

        public void FadeInEndGamePanel()
        {
            endGamePanel.FadeIn(endGamePanelFadeInDelay, endGamePanelFadeInTreshold);
        }
        public void FadeOutEndGamePanel()
        {
            endGamePanel.FadeOut(endGamePanelFadeOutDelay);
        }

        public void SetEndGameTextActiveness(bool avctiveness)
        {
            endGameText.gameObject.SetActive(avctiveness);
        }

        public void SetEndGameText()
        {
            endGameText.SetText(endGameTextToSet);
        }

        public void ResetEndGameText()
        {
            endGameText.ResetText();
        }

        #endregion

    }

}