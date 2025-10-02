using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.UI
{

    public class UI_ReloadBar : MonoBehaviour
    {

        private Vector2 startSize = default;

        [SerializeField]
        private Image reloadBar = null;
        [SerializeField]
        private Transform ammoContainer = null;
        [SerializeField]
        private GameObject prefabAmmoImage = null;

        public void SetUp(int ammoCount)
        {
            HorizontalLayoutGroup layout = ammoContainer.GetComponent<HorizontalLayoutGroup>();
            RectTransform rect = prefabAmmoImage.GetComponent<RectTransform>();
            float offsetX = (rect.sizeDelta.x * ammoCount) + (layout.spacing * (ammoCount - 1));
            reloadBar.rectTransform.sizeDelta = new Vector2(offsetX, reloadBar.rectTransform.sizeDelta.y);
            startSize = reloadBar.rectTransform.sizeDelta;
        }
        public void DecreaseReloadBar(float reloadTime)
        {
            StartCoroutine(DecreaseScaleXTimed(reloadBar, reloadTime));
        }
        public void ResetReloadBar()
        {
            reloadBar.rectTransform.sizeDelta = startSize;
        }

        private IEnumerator DecreaseScaleXTimed(Image image, float time)
        {
            float scaleX = 0;
            float timeElapsed = 0.0f;
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;
                scaleX = Mathf.Lerp(startSize.x, 0, timeElapsed / time);
                image.rectTransform.sizeDelta = new Vector2(scaleX, image.rectTransform.sizeDelta.y);
                yield return null;
            }
        }

    }

}