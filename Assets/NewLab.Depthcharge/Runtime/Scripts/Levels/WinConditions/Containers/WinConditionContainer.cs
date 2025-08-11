using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/WinConditionContainer")]
    public class WinConditionContainer : ScriptableObject
    {

        [SerializeField]
        private BaseWinStrategy _strategy;
        public BaseWinStrategy Strategy { get => _strategy; }

        [SerializeField]
        private string _description = string.Empty;
        public string Description { get => $"{_strategy.NumberToCompare} {_description}"; }

        [SerializeField]
        private Sprite _image = null;
        public Sprite Image { get => _image; }

    }

}