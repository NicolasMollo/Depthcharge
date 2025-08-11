using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/WinStrategies/WS_EnemiesDefeated")]
    public class WS_EnemiesDefeated : BaseWinStrategy
    {

        public override bool WinLevelCondition(BaseLevelController levelController)
        {
            return levelController.Stats.EnemiesDefeated == _numberToCompare;
        }

    }

}