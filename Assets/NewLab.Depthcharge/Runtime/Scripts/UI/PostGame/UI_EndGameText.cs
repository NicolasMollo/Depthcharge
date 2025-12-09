using System;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI.EndGame
{
    public enum EndGameTextType { Defeated, Missed, Score, Time, Default }
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UI_EndGameText : MonoBehaviour
    {

        private TextMeshProUGUI tmpText = null;
        [SerializeField]
        private EndGameTextType _type = EndGameTextType.Defeated;
        public EndGameTextType Type { get => _type; }
        [SerializeField]
        private BaseTextStrategy strategy = null;
        public bool IsSet { get; set; } = false;

        private void Awake()
        {
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text, Action<bool> onSetText = null)
        {
            strategy.SetText(tmpText, text, onSetText);
        }

        public void ResetText()
        {
            tmpText.text = string.Empty;
        }

    }

}