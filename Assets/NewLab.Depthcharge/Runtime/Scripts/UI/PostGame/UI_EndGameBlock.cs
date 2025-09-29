using UnityEngine;

namespace Depthcharge.UI.EndGame
{

    [DisallowMultipleComponent]
    public class UI_EndGameBlock : MonoBehaviour
    {
        [SerializeField]
        private EndGameTextType _type = EndGameTextType.Defeated;
        public EndGameTextType Type { get => _type; }
    }

}