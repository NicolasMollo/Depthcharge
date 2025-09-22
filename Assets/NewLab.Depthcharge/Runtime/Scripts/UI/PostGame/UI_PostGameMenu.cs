using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UI_PostGameMenu : MonoBehaviour
    {

        private List<BaseButtonAdapter> _buttons = null;
        public List<BaseButtonAdapter> Buttons { get => _buttons; }
        [Header("BUTTONS")]
        [SerializeField]
        private UI_SceneButtonAdapter reloadButton = null;
        [SerializeField]
        private UI_SceneButtonAdapter quitButton = null;
        [Header("TEXTS")]
        [SerializeField]
        private TextMeshProUGUI defeatedText = null;
        [SerializeField]
        private TextMeshProUGUI missedText = null;
        [SerializeField]
        private TextMeshProUGUI scoreText = null;
        [SerializeField]
        private TextMeshProUGUI timeText = null;
        [Header("TEXT STRATEGIES")]
        [SerializeField]
        private BaseTextStrategy timeTextStrategy = null;
        [SerializeField]
        private BaseTextStrategy defeatedTextStrategy = null;
        [SerializeField]
        private BaseTextStrategy missedTextStrategy = null;
        [SerializeField]
        private BaseTextStrategy scoreTextStrategy = null;
        [Header("BLOCK OBJECTS")]
        private GameObject timeBlock = null;

        public void SetUp()
        {
            _buttons = new List<BaseButtonAdapter>();
            _buttons.Add(reloadButton);
            _buttons.Add(quitButton);
        }
        public void SetDefeatedText(string text)
        {
            defeatedTextStrategy.SetText(defeatedText, text);
        }
        public void SetMissedText(string text)
        {
            missedTextStrategy.SetText(missedText, text);
        }
        public void SetScoreText(string text)
        {
            scoreTextStrategy.SetText(scoreText, text);
        }
        public void SetTimeText(string text)
        {
            timeTextStrategy.SetText(timeText, text);
        }

        public void ActivateTimeBlock()
        {
            timeBlock.SetActive(true);
        }
        public void DeactivateTimeBlock()
        {
            timeBlock.SetActive(false);
        }

        public void SubscribeToReloadButton(Action<SceneConfiguration> method)
        {
            reloadButton.OnClick += method;
        }
        public void UnsubscribeFromReloadButton(Action<SceneConfiguration> method)
        {
            reloadButton.OnClick -= method;
        }

        public void SubscribeToQuitButton(Action<SceneConfiguration> method)
        {
            quitButton.OnClick += method;
        }
        public void UnsubscribeFromQuitButton(Action<SceneConfiguration> method)
        {
            quitButton.OnClick -= method;
        }
        public void SetQuitButtonArg(SceneConfiguration arg)
        {
            quitButton.SetOnClickArg(arg);
        }
        public void SetReloadButtonArg(SceneConfiguration arg)
        {
            reloadButton.SetOnClickArg(arg);
        }

    }

}