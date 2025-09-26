using Depthcharge.SceneManagement;
using System;
using System.Collections.Generic;
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
        private UI_FlaggedTMPText defeatedText = null;
        public bool defeatedSetState { get => defeatedText.IsSet; }
        [SerializeField]
        private UI_FlaggedTMPText missedText = null;
        public bool MissedTextSetState { get => missedText.IsSet; }
        [SerializeField]
        private UI_FlaggedTMPText scoreText = null;
        public bool ScoreTextSetState { get => scoreText.IsSet; }
        [SerializeField]
        private UI_FlaggedTMPText timeText = null;
        public bool TimeTextSetState { get => timeText.IsSet; }
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
        [SerializeField]
        private GameObject timeBlock = null;
        [SerializeField]
        private GameObject defeatedBlock = null;
        [SerializeField]
        private GameObject missedBlock = null;
        [SerializeField]
        private GameObject scoreBlock = null;

        private void Awake()
        {
            _buttons = new List<BaseButtonAdapter>();
            _buttons.Add(reloadButton);
            _buttons.Add(quitButton);
        }

        public void SetDefeatedText(string text)
        {
            defeatedTextStrategy.SetText(defeatedText.Text, text);
        }
        public void SetMissedText(string text)
        {
            missedTextStrategy.SetText(missedText.Text, text);
        }
        public void SetScoreText(string text)
        {
            scoreTextStrategy.SetText(scoreText.Text, text);
        }
        public void SetTimeText(string text)
        {
            timeTextStrategy.SetText(timeText.Text, text);
        }

        public void SetAllFlaggedTextState(bool enablingState)
        {
            defeatedText.IsSet = enablingState;
            missedText.IsSet = enablingState;
            scoreText.IsSet = enablingState;
            if (timeText != null) timeText.IsSet = enablingState;
        }

        public void SetTimeBlockActiveness(bool activeness)
        {
            timeBlock.SetActive(activeness);
        }
        public void SetDefeatedBlockActiveness(bool activeness)
        {
            defeatedBlock.SetActive(activeness);
        }
        public void SetMissedBlockActiveness(bool activeness)
        {
            missedBlock.SetActive(activeness);
        }
        public void SetScoreBlockActiveness(bool activeness)
        {
            scoreBlock.SetActive(activeness);
        }

        public void SetReloadButtonActiveness(bool activeness)
        {
            reloadButton.gameObject.SetActive(activeness);
        }
        public void SetQuitButtonActiveness(bool activeness)
        {
            quitButton.gameObject.SetActive(activeness);
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