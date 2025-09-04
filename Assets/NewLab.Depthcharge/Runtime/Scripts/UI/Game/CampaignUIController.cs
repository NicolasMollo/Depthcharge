using Depthcharge.LevelManagement;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{

    public class CampaignUIController : BaseGameUIController
    {

        [Header("TEXTS")]
        [SerializeField]
        private string levelTextRoot = string.Empty;
        [SerializeField]
        private TextMeshProUGUI levelText = null;
        [SerializeField]
        private TextMeshProUGUI winConditionText = null;

        public override void SetUp(GameUIContext context)
        {
            base.SetUp(context);
            SetLevelText(context.levelNumber.ToString());
            SetScoreText(context.levelController.Stats.Score.ToString());
            SetWinConditionText(
                context.levelController.Configuration.Difficulty, 
                context.levelController.WinCondition.Description
                );
            SetEnemiesText(context.levelController.Stats.EnemiesDefeated.ToString());
        }

        public void SetLevelText(string text)
        {
            levelText.text = $"{levelTextRoot} {text}";
        }

        public void SetWinConditionText(LevelConfiguration.LevelDifficulty difficulty, string text)
        {
            switch (difficulty)
            {
                case LevelConfiguration.LevelDifficulty.Easy:
                    winConditionText.color = Color.green;
                    break;
                case LevelConfiguration.LevelDifficulty.Normal:
                    winConditionText.color = Color.yellow;
                    break;
                case LevelConfiguration.LevelDifficulty.Hard:
                    winConditionText.color = Color.red;
                    break;
            }
            string textToSet = $"{difficulty.ToString()} | {text}";
            winConditionText.text = textToSet;
        }

    }

}