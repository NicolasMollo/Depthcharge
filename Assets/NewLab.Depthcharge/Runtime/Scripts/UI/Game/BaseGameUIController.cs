using TMPro;
using UnityEngine;

namespace Depthcharge.UI
{

    public abstract class BaseGameUIController : MonoBehaviour
    {

        #region UI elements

        [Header("UI ELEMENTS (Base)")]
        [SerializeField]
        protected UI_HealthBar healthBar = null;
        [SerializeField]
        protected UI_Ammo ammo = null;
        [SerializeField]
        protected UI_ReloadBar reloadBar = null;

        #endregion
        #region Texts

        [Header("TEXTS (Base)")]
        [SerializeField]
        protected TextMeshProUGUI scoreText = null;
        [SerializeField]
        protected TextMeshProUGUI enemiesText = null;

        #endregion
        #region Settings

        [Header("SETTINGS (Base)")]
        [SerializeField]
        [Range(5, 10)]
        private int scoreTextZeros = 0;
        [SerializeField]
        private float ammoTransparency = 1.0f;
        [SerializeField]
        private float healthBarWidth = 0.0f;

        #endregion

        public virtual void SetUp(GameUIContext context)
        {
            healthBar.SetUp(context.player.HealthModule.HealthPercentage, healthBarWidth);
            ammo.SetUp(context.player.ShootModule.PoolSize, ammoTransparency);
            reloadBar.SetUp(context.player.ShootModule.PoolSize);
        }
        public virtual void CleanUp(GameUIContext context) { }

        public virtual void UpdateHealthBar(float healthPercentage)
        {
            healthBar.UpdateHealthBar(healthPercentage);
        }
        public virtual void DecreaseReloadBar(float reloadTime)
        {
            reloadBar.DecreaseReloadBar(reloadTime);
        }
        public virtual void ResetReloadBar()
        {
            reloadBar.ResetReloadBar();
        }
        public virtual void AddAmmoTransparency()
        {
            ammo.AddTransparency();
        }
        public virtual void RemoveAmmoTransparency()
        {
            ammo.RemoveTransparency();
        }

        public virtual void SetScoreText(string text)
        {
            scoreText.text = text.PadLeft(scoreTextZeros, '0');
        }
        public virtual void SetEnemiesText(string text)
        {
            enemiesText.text = text;
        }

    }

}