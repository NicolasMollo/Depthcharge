using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI/TextStrategies/TS_ImmediateText")]
    public class TS_ImmediateText : BaseTextStrategy
    {
        public override void SetText(TextMeshProUGUI textToSet, string text)
        {
            textToSet.text = text;
            UI_FlaggedTMPText flaggedText = textToSet.GetComponent<UI_FlaggedTMPText>();
            if (flaggedText != null)
            {
                flaggedText.IsSet = true;
            }
        }

    }
}