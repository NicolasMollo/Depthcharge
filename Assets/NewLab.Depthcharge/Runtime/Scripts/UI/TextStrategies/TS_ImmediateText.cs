using Depthcharge.UI.EndGame;
using System;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI/TextStrategies/TS_ImmediateText")]
    public class TS_ImmediateText : BaseTextStrategy
    {
        public override void SetText(TextMeshProUGUI textToSet, string text, Action<bool> onSetText = null)
        {
            textToSet.text = text;
            bool isUpdatedText = false;
            onSetText?.Invoke(isUpdatedText);
            UI_EndGameText endGameText = textToSet.GetComponent<UI_EndGameText>();
            if (endGameText != null)
            {
                endGameText.IsSet = true;
            }
        }

    }
}