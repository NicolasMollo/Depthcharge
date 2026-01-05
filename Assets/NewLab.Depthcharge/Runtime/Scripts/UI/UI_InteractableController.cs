using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.UI
{

    [RequireComponent(typeof(UI_InputController), typeof(UI_SelectionController))]
    public abstract class UI_InteractableController : MonoBehaviour
    {

        protected UI_InputController input = null;
        protected UI_SelectionController _selection = null;
        public UI_SelectionController Selection { get => _selection; }

        [SerializeField]
        protected UI_Selector selector = null;
        [SerializeField]
        protected List<BaseButtonAdapter> buttons = null;


        protected virtual void Awake()
        {
            input = GetComponent<UI_InputController>();
            _selection = GetComponent<UI_SelectionController>();
            string message = $"=== {GetType().ToString()}.Awake() === Be ensure to fill \"selector\" field in Inspector!";
            Assert.IsNotNull(selector, message);
            message = $"=== {GetType().ToString()}.Awake() === Be ensure to fill \"buttons\" list field in Inspector!";
            Assert.IsTrue(buttons.Count > 0, message);
        }

        protected virtual void Start()
        {
            SelectionContext selectionContext = new SelectionContext(buttons, input, selector);
            _selection.SetUp(selectionContext);
        }

        public void EnableInput()
        {
            input.EnableInput();
            _selection.SubscribeToInput();
        }
        public void DisableInput()
        {
            _selection.UnsubscribeFromInput();
            input.DisableInput();
        }

        public void ResetSelection()
        {
            _selection.ResetSelection();
        }

        public BaseButtonAdapter GetButtonOfType(ButtonType type)
        {
            foreach (BaseButtonAdapter button in buttons)
            {
                if (button.Type == type)
                {
                    return button;
                }
            }
            string message = $"=== {GetType().ToString()}.GetButtonOfType() === Button of type {type.ToString()} not found!";
            Assert.IsTrue(false, message);
            return null;
        }

    }

}