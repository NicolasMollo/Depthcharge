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

        public virtual void SetUp()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }
        public virtual void CleanUp()
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