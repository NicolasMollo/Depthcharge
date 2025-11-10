using Depthcharge.UI.EndGame;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI/TextStrategies/TS_UpdatedNumericText")]
    public class TS_UpdatedNumericText : BaseTextStrategy
    {

        [SerializeField]
        private float delay = 0f;

        public override void SetText(TextMeshProUGUI textToSet, string text)
        {
            int castedText = int.Parse(text);
            MonoBehaviour monoBehaviour = textToSet.GetComponent<MonoBehaviour>();
            string message = $"=== {this.name} === The owner of this strategy it's not a MonoBehaviour!";
            Assert.IsNotNull(monoBehaviour, message);
            monoBehaviour.StartCoroutine(UpdateText(textToSet, castedText));
        }

        private IEnumerator UpdateText(TextMeshProUGUI textToSet, int castedText)
        {
            for (int i = 0; i <= castedText; i++)
            {
                textToSet.text = i.ToString();
                yield return new WaitForSeconds(delay);
            }
            UI_EndGameText endGameText = textToSet.GetComponent<UI_EndGameText>();
            if (endGameText != null)
                endGameText.IsSet = true;
        }

    }
}