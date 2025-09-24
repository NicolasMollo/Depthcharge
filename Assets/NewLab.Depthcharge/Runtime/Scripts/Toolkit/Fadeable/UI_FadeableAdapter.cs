using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Depthcharge.Toolkit
{
    public class UI_FadeableAdapter : BaseFadeableAdapter
    {

        [SerializeField]
        private MaskableGraphic maskableGraphic = null;

        public override void FadeIn(float delay, float treshold)
        {
            StartCoroutine(InternalFadeIn(delay, treshold));
        }
        protected override IEnumerator InternalFadeIn(float delay, float treshold)
        {
            float tresholdNormalized = 0.0f;
            const float OFFSET = 0.01f;
            if (treshold > 1)
                tresholdNormalized = (treshold * 1.0f) / 255.0f;
            else
                tresholdNormalized = treshold;
            for (float i = 0; i < tresholdNormalized; i += OFFSET)
            {
                maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, i);
                yield return new WaitForSeconds(delay);
            }
            IsFadedIn = true;
        }

        public override void FadeOut(float delay)
        {
            StartCoroutine(InternalFadeOut(delay));
        }
        protected override IEnumerator InternalFadeOut(float delay)
        {
            const float OFFSET = 0.01f;
            for (float i = maskableGraphic.color.a; i > 0; i -= OFFSET)
            {
                maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, i);
                yield return new WaitForSeconds(delay);
            }
            IsFadedIn = false;
        }

    }
}