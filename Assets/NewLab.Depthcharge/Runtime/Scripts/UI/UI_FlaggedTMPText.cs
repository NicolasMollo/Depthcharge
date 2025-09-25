using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UI_FlaggedTMPText : MonoBehaviour
    {
        private TextMeshProUGUI text = null;
        public TextMeshProUGUI Text { get => text; set => text = value; }
        public bool IsSet { get; set; } = false;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }

}