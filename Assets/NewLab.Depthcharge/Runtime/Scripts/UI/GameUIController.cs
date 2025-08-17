using Depthcharge.LevelManagement;
using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{

    public class GameUIController : MonoBehaviour
    {

        #region UI elements

        [Header("UI ELEMENTS")]
        [SerializeField]
        private UI_HealthBar healthBar = null;
        [SerializeField]
        private UI_Ammo ammo = null;
        [SerializeField]
        private UI_ReloadBar reloadBar = null;

        #endregion
        #region Texts

        [Header("TEXTS")]
        [SerializeField]
        private TextMeshProUGUI scoreText = null;
        [SerializeField]
        private TextMeshProUGUI winConditionText = null;
        [SerializeField]
        private TextMeshProUGUI enemiesText = null;
        [SerializeField]
        private TextMeshProUGUI levelText = null;

        #endregion
        #region Settings

        [Header("SETTINGS")]
        [SerializeField]
        [Range(5, 10)]
        private int scoreTextZeros = 0;
        [SerializeField]
        private string levelTextRoot = string.Empty;
        [SerializeField]
        private float ammoTransparency = 1.0f;
        [SerializeField]
        private float healthBarWidth = 0.0f;

        #endregion

        public void SetUp(float healthPercentage, int ammoCount)
        {
            healthBar.SetUp(healthPercentage, healthBarWidth);
            ammo.SetUp(ammoCount, ammoTransparency);
            reloadBar.SetUp(ammoCount);
        }

        public void UpdateHealthBar(float healthPercentage)
        {
            healthBar.UpdateHealthBar(healthPercentage);
        }
        public void DecreaseReloadBar(float reloadTime)
        {
            reloadBar.DecreaseReloadBar(reloadTime);
        }
        public void ResetReloadBar()
        {
            reloadBar.ResetReloadBar();
        }
        public void AddAmmoTransparency()
        {
            ammo.AddTransparency();
        }
        public void RemoveAmmoTransparency()
        {
            ammo.RemoveTransparency();
        }

        public void SetScoreText(string text)
        {
            scoreText.text = text.PadLeft(scoreTextZeros, '0');
        }
        public void SetEnemiesText(string text)
        {
            enemiesText.text = text;
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

        public void SetLevelText(string text)
        {
            levelText.text = $"{levelTextRoot} {text}";
        }

    }

}