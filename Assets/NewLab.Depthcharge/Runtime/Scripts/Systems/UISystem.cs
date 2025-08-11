using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Depthcharge.LevelManagement;
using System.Collections.Generic;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UISystem : MonoBehaviour
    {

        [SerializeField]
        private TextMeshProUGUI scoreText = null;
        [SerializeField]
        private TextMeshProUGUI winConditionText = null;
        [SerializeField]
        private TextMeshProUGUI enemiesText = null;
        [SerializeField]
        private TextMeshProUGUI levelText = null;
        [SerializeField]
        private Transform ammoContainer = null;
        [SerializeField]
        private GameObject prefabAmmoImage = null;
        private List<Image> ammoImages = null;

        [SerializeField]
        [Range(5, 10)]
        private int scoreTextZeros = 0;

        [SerializeField]
        private string levelTextRoot = string.Empty;

        #region API

        public void InitAmmoImages(int ammoCount)
        {
            ammoImages = new List<Image>();
            Image temporary = null;
            for (int i = 0; i < ammoCount; i++)
            {
                temporary = CreateAmmoImage();
                ammoImages.Add(temporary);
            }
            ammoImages.Reverse();
        }

        private Image CreateAmmoImage()
        {
            GameObject ammoObj = Instantiate(prefabAmmoImage, ammoContainer);
            Image ammoImage = ammoObj.GetComponent<Image>();
            return ammoImage;
        }

        public void DisableAmmoImage()
        {
            foreach (Image image in ammoImages)
            {
                if (image.gameObject.activeSelf)
                {
                    image.gameObject.SetActive(!image.gameObject.activeSelf);
                    return;
                }
            }
        }

        public void EnableAllAmmoImages()
        {
            foreach (Image image in ammoImages)
            {
                image.gameObject.SetActive(true);
            }
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

        #endregion

    }

}