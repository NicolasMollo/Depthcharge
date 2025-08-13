using Depthcharge.LevelManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private float imagesAlphaValue = 1.0f;
        [SerializeField]
        private Image reloadBar = null;
        private Vector2 reloadBarStartSize = default;

        [SerializeField]
        [Range(5, 10)]
        private int scoreTextZeros = 0;

        [SerializeField]
        private string levelTextRoot = string.Empty;



        #region API

        public void DecreaseReloadBar(float time)
        {
            StartCoroutine(DecreaseScaleXTimed(reloadBar, time));
        }
        private IEnumerator DecreaseScaleXTimed(Image image, float time)
        {
            float scaleX = 0;
            float timeElapsed = 0.0f;
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                scaleX = Mathf.Lerp(reloadBarStartSize.x, 0, timeElapsed / time);
                image.rectTransform.sizeDelta = new Vector2(scaleX, image.rectTransform.sizeDelta.y);
                // image.transform.localScale = new Vector2(scaleX, image.transform.localScale.y);
                yield return null;
            }
        }

        public void ResetReloadBar()
        {
            reloadBar.rectTransform.sizeDelta = reloadBarStartSize;
        }
        public void SetReloadBar(int ammoNumber)
        {
            HorizontalLayoutGroup layout = ammoContainer.GetComponent<HorizontalLayoutGroup>();
            RectTransform prefabAmmoRectTransform = prefabAmmoImage.GetComponent<RectTransform>();
            float offsetX = (prefabAmmoRectTransform.sizeDelta.x * ammoNumber) + (layout.spacing * (ammoNumber - 1));
            reloadBar.rectTransform.sizeDelta = new Vector2(offsetX, reloadBar.rectTransform.sizeDelta.y);
            reloadBarStartSize = reloadBar.rectTransform.sizeDelta;
        }


        public void InitAmmoImages(int ammoCount)
        {
            ammoImages = new List<Image>();
            Image temporary = null;
            for (int i = 0; i < ammoCount; i++)
            {
                temporary = CreateAmmoImage();
                ammoImages.Add(temporary);
            }
        }

        private Image CreateAmmoImage()
        {
            GameObject ammoObj = Instantiate(prefabAmmoImage, ammoContainer);
            Image ammoImage = ammoObj.GetComponent<Image>();
            return ammoImage;
        }

        public void AddAmmoImageTransparency()
        {
            float alpha = imagesAlphaValue;
            Color newColor = default;
            foreach (Image image in ammoImages)
            {
                if (image.color.a != alpha)
                {
                    newColor = new Color(image.color.r, image.color.g, image.color.b, alpha);
                    image.color = newColor;
                    return;
                }
            }
        }

        public void RemoveAmmoImagesTransparency()
        {
            float alpha = 1.0f;
            Color newColor = new Color(
                ammoImages[0].color.r, 
                ammoImages[0].color.g, 
                ammoImages[0].color.b, 
                alpha
                );
            foreach (Image image in ammoImages)
                image.color = newColor;
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