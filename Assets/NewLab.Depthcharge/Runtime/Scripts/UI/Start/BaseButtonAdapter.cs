using System;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public abstract class BaseButtonAdapter<T> : MonoBehaviour
    {

        protected Button button = null;
        public Action<T> OnClick = null;
        [SerializeField]
        protected T onClickArg = default;

        public virtual void SetUp()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }
        public virtual void CleanUp()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public virtual void OnButtonClick()
        {
            OnClick?.Invoke(onClickArg);
        }

    }

}