using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    public class UI_Selector : MonoBehaviour
    {

        [SerializeField]
        private Image selector = null;

        public void SetSelectorPosition(Vector2 position)
        {
            selector.rectTransform.position = position;
        }

        public void SetSelectorPosition(float positionY)
        {
            Vector2 position = new Vector2(selector.rectTransform.position.x, positionY);
            selector.rectTransform.position = position;
        }

    }

}