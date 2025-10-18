using Depthcharge.LevelManagement;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{
    [DisallowMultipleComponent]
    public class BossUIController : BaseGameUIController
    {

        [Header("SETTINGS (BOSS)")]
        [SerializeField]
        private UI_HealthBar bossHealthBar = null;
        [SerializeField]
        private float bossHealthBarWidth = 0.0f;
        [SerializeField]
        private TextMeshProUGUI bossNameText = null;

        public override void SetUp(GameUIContext context)
        {
            base.SetUp(context);
            BossLevelController level = context.levelController as BossLevelController;
            if (level == null)
            {
                Debug.LogError($"=== BossUIController.SetUp() === levelController is not a BossLevelController!");
                return;
            }
            bossHealthBar.SetUp(level.Boss.HealthModule.HealthPercentage, bossHealthBarWidth);
            bossNameText.text = level.Boss.EnemyName;
        }

        public void UpdateBossHealthBar(float healthPercentage)
        {
            bossHealthBar.UpdateHealthBar(healthPercentage);
        }
        public void SetBossHealthBarColor(Color color)
        {
            bossHealthBar.SetColor(color);
        }

    }
}