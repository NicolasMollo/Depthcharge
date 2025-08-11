using UnityEngine;

namespace Depthcharge.LevelManagement
{
    public abstract class BaseWinStrategy : ScriptableObject
    {

        [SerializeField]
        [Range(-1, 1000)]
        protected int _numberToCompare = 0;
        public int NumberToCompare { get => _numberToCompare; }
        public abstract bool WinLevelCondition(BaseLevelController levelController);

    }
}