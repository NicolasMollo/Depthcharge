using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    public abstract class BaseTextStrategy : ScriptableObject
    {
        public abstract void SetText(TextMeshProUGUI textToSet, string text);
    }
}