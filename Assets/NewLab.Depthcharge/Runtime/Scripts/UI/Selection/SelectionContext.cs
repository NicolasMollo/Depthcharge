using System.Collections.Generic;

namespace Depthcharge.UI
{
    public struct SelectionContext
    {
        public List<BaseButtonAdapter> buttons;
        public UI_InputController input;
        public UI_Selector selector;

        public SelectionContext(List<BaseButtonAdapter> buttons, UI_InputController input, UI_Selector selector)
        {
            this.buttons = buttons;
            this.input = input;
            this.selector = selector;
        }
    }
}