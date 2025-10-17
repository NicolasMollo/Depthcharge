using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    public class UI_HealthBar : MonoBehaviour
    {

        [SerializeField]
        private Image healthBar = null;
        [SerializeField]
        private float fixedWidth = 10.0f;
        private float startWidth = 1.0f;

        public void SetUp(float healthPercentage, float fixedWidth = default)
        {
            this.fixedWidth = fixedWidth != default ? fixedWidth : this.fixedWidth;
            const float START_HEALTH_PERCENTAGE = 1.0f;
            startWidth = START_HEALTH_PERCENTAGE * fixedWidth;
            UpdateHealthBar(healthPercentage);
        }

        public void UpdateHealthBar(float healthPercentage)
        {
            float calculatedWidth = healthPercentage * fixedWidth;
            healthBar.rectTransform.sizeDelta = new Vector2(calculatedWidth, healthBar.rectTransform.sizeDelta.y);
            SetHealthBarColor(healthBar.rectTransform.sizeDelta.x);
        }

        private void SetHealthBarColor(float currentWidth)
        {
            const float MID_HEALTH_PERCENTAGE = 50.0f;
            const float LOW_HEALTH_PERCENTAGE = 15.0f;
            float yellowLimit = startWidth * MID_HEALTH_PERCENTAGE * 0.01f;
            float redLimit = startWidth * LOW_HEALTH_PERCENTAGE * 0.01f;

            if (currentWidth > yellowLimit)
            {
                healthBar.color = Color.green;
            }
            else if (currentWidth > redLimit && currentWidth <= yellowLimit)
            {
                healthBar.color = Color.yellow;
            }
            else
            {
                healthBar.color = Color.red;
            }
        }

    }

}