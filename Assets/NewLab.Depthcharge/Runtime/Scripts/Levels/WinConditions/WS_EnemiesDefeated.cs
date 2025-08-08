using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/WinStrategies/WS_EnemiesDefeated")]
    public class WS_EnemiesDefeated : BaseWinStrategy
    {

        [SerializeField]
        [Range(-1, 50)]
        private int enemiesToDefeat = 1;

        public override bool WinLevelCondition(BaseLevelController levelController)
        {
            return levelController.Stats.EnemiesDefeated == enemiesToDefeat;
        }

    }

}