using System.Collections;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI/TextStrategies/TS_UpdatedText")]
    public class TS_UpdatedText : BaseTextStrategy
    {

        [SerializeField]
        private float delay = 0f;

        public override void SetText(TextMeshProUGUI textToSet, string text)
        {
            int castedText = int.Parse(text);
            MonoBehaviour monoBehaviour = textToSet.GetComponent<MonoBehaviour>();
            if (monoBehaviour == null)
            {
                Debug.LogError($"=== {this.name} === The owner of this strategy it's not a MonoBehaviour!");
                return;
            }
            monoBehaviour.StartCoroutine(UpdateText(textToSet, castedText));
        }

        private IEnumerator UpdateText(TextMeshProUGUI textToSet, int castedText)
        {
            for (int i = 0; i < castedText; i++)
            {
                textToSet.text = i.ToString();
                yield return new WaitForSeconds(delay);
            }
        }

    }
}