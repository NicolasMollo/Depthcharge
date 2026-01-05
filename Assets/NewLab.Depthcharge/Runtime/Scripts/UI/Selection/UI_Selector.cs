using System;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    public class UI_Selector : MonoBehaviour
    {

        [SerializeField]
        private Image selector = null;

        public Action OnSelectorPositioned = null;

        public void SetSelectorPosition(Vector2 position)
        {
            selector.rectTransform.position = position;
            OnSelectorPositioned?.Invoke();
        }

        public void SetSelectorPosition(float positionY)
        {
            Vector2 position = new Vector2(selector.rectTransform.position.x, positionY);
            selector.rectTransform.position = position;
            OnSelectorPositioned?.Invoke();
        }

    }

}