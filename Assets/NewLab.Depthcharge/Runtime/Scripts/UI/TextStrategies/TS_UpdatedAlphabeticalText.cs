using Depthcharge.UI.EndGame;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI/TextStrategies/TS_UpdatedAlphabeticalText")]
    public class TS_UpdatedAlphabeticalText : BaseTextStrategy
    {

        [SerializeField]
        private float delay = 1.0f;

        public override void SetText(TextMeshProUGUI textToSet, string text, Action<bool> onSetText = null)
        {
            MonoBehaviour monoBehaviour = textToSet.GetComponent<MonoBehaviour>();
            string message = $"=== {this.name} === The owner of this strategy it's not a MonoBehaviour!";
            Assert.IsNotNull(monoBehaviour, message);
            monoBehaviour.StartCoroutine(UpdateText(textToSet, text, onSetText));
        }

        private IEnumerator UpdateText(TextMeshProUGUI textToSet, string text, Action<bool> onSetText = null)
        {
            bool isUpdatedText = true;
            for (int i = 0; i < text.Length; i++)
            {
                textToSet.text += text[i];
                onSetText?.Invoke(isUpdatedText);
                yield return new WaitForSeconds(delay);
            }
            UI_EndGameText endGameText = textToSet.GetComponent<UI_EndGameText>();
            if (endGameText != null)
            {
                endGameText.IsSet = true;
            }
        }

    }
}