using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public abstract class BaseButtonAdapter : MonoBehaviour, IPointerEnterHandler
    {

        protected Button button = null;
        public Image Image { get => button.image; }
        public Action<BaseButtonAdapter> OnHover = null;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public abstract void OnButtonClick();
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHover?.Invoke(this);
        }

    }

}