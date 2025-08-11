using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/WinStrategies/WS_ScoreAmount")]
    public class WS_ScoreAmount : BaseWinStrategy
    {

        public override bool WinLevelCondition(BaseLevelController levelController)
        {
            return levelController.Stats.Score >= _numberToCompare;
        }

    }

}