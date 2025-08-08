using UnityEngine;

namespace Depthcharge.LevelManagement
{
    public abstract class BaseWinStrategy : ScriptableObject
    {

        public abstract bool WinLevelCondition(BaseLevelController levelController);

    }
}