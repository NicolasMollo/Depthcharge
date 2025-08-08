using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/WinStrategies/WS_ScoreAmount")]
    public class WS_ScoreAmount : BaseWinStrategy
    {

        [SerializeField]
        [Range(20, 500)]
        private int scoreAmount = 20;

        public override bool WinLevelCondition(BaseLevelController levelController)
        {
            return levelController.Stats.Score >= scoreAmount;
        }

    }

}