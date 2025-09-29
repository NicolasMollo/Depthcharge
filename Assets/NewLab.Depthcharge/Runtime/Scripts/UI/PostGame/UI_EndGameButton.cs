using UnityEngine;

namespace Depthcharge.UI.EndGame
{
    public enum EndGameButtonType { Reload, Quit }
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UI_SceneButtonAdapter))]
    public class UI_EndGameButton : MonoBehaviour
    {

        private UI_SceneButtonAdapter _button = null;
        public UI_SceneButtonAdapter Button { get => _button; }
        [SerializeField]
        private EndGameButtonType _type = EndGameButtonType.Reload;
        public EndGameButtonType Type { get => _type; }

        private void Awake()
        {
            _button = GetComponent<UI_SceneButtonAdapter>();
        }

    }
}