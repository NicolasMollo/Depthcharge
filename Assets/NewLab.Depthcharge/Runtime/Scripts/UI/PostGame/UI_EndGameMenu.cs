using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.UI.EndGame
{

    [DisallowMultipleComponent]
    public class UI_EndGameMenu : MonoBehaviour
    {

        [SerializeField]
        private List<UI_EndGameText> texts = null;
        public List<UI_EndGameText> Texts { get => texts; }
        [SerializeField]
        private List<UI_EndGameButton> buttons = null;
        public List<UI_EndGameButton> Buttons { get => buttons; }
        [SerializeField]
        private List<UI_EndGameBlock> blocks = null;

        public bool AreTextsConfigured
        {
            get
            {
                foreach (UI_EndGameText text in texts)
                {
                    if (!text.IsSet)
                        return text.IsSet;
                }
                return true;
            }
        }

        private void Awake()
        {
            texts.Sort((a, b) => a.Type.CompareTo(b.Type));
            buttons.Sort((a, b) => a.Type.CompareTo(b.Type));
            blocks.Sort((a, b) => a.Type.CompareTo(b.Type));
        }

        #region Texts

        public UI_EndGameText SetText(EndGameTextType type, string text)
        {
            UI_EndGameText endGameText = GetEndGameText(type);
            endGameText.SetText(text);
            return endGameText;
        }
        public void SetTextActiveness(EndGameTextType type, bool activeness)
        {
            UI_EndGameText endGameText = GetEndGameText(type);
            endGameText.gameObject.SetActive(activeness);
        }
        public void SetAllTextsState(bool enablingState)
        {
            foreach (UI_EndGameText text in texts)
                text.IsSet = enablingState;
        }
        private UI_EndGameText GetEndGameText(EndGameTextType type)
        {
            UI_EndGameText endGameText = texts[(int)type];
            if (endGameText == null)
            {
                Debug.LogError($"=== UI_EndGameMenu === text doesn't exist!");
                return null;
            }
            return endGameText;
        }

        #endregion
        #region Buttons

        public void SetAllButtonsActiveness(bool activeness)
        {
            foreach (UI_EndGameButton button in buttons)
                button.gameObject.SetActive(activeness);
        }
        public void SetButtonActiveness(EndGameButtonType type, bool activeness)
        {
            UI_EndGameButton endGameButton = GetEndGameButton(type);
            endGameButton.gameObject.SetActive(activeness);
        }
        public void SubscribeToButton(EndGameButtonType type, Action<SceneConfiguration> method)
        {
            UI_EndGameButton endGameButton = GetEndGameButton(type);
            endGameButton.Button.OnClick += method;
        }
        public void UnsubscribeToButton(EndGameButtonType type, Action<SceneConfiguration> method)
        {
            UI_EndGameButton endGameButton = GetEndGameButton(type);
            endGameButton.Button.OnClick -= method;
        }
        public void SetButtonArg(EndGameButtonType type, SceneConfiguration arg)
        {
            UI_EndGameButton endGameButton = GetEndGameButton(type);
            endGameButton.Button.SetOnClickArg(arg);
        }
        private UI_EndGameButton GetEndGameButton(EndGameButtonType type)
        {
            UI_EndGameButton endGameButton = buttons[(int)type];
            if (endGameButton == null)
            {
                Debug.LogError($"=== UI_EndGameMenu === button doesn't exist!");
                return null;
            }
            return endGameButton;
        }

        #endregion
        #region Blocks

        public void SetAllBlocksActiveness(bool activeness)
        {
            foreach (UI_EndGameBlock block in blocks)
                block.gameObject.SetActive(activeness);
        }
        public void SetBlockActiveness(EndGameTextType type, bool activeness)
        {
            UI_EndGameBlock block = GetEndGameBlock(type);
            block.gameObject.SetActive(activeness);
        }
        private UI_EndGameBlock GetEndGameBlock(EndGameTextType type)
        {
            UI_EndGameBlock endGameBlock = blocks[(int)type];
            if (endGameBlock == null)
            {
                Debug.LogError($"=== UI_EndGameMenu === block doesn't exist!");
                return null;
            }
            return endGameBlock;
        }

        #endregion

    }

}