using System;
using UnityEngine;

namespace Depthcharge.UI
{
    public abstract class GenericButtonAdapter<T> : BaseButtonAdapter
    {

        public Action<T> OnClick = null;
        [SerializeField]
        protected T onClickArg = default;

        public void SetOnClickArg(T arg)
        {
            onClickArg = arg;
        }
        public override void OnButtonClick()
        {
            OnClick?.Invoke(onClickArg);
        }

    }
}