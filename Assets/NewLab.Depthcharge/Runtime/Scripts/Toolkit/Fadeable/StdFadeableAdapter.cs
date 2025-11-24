using System.Collections;
using UnityEngine;

namespace Depthcharge.Toolkit
{
    public class StdFadeableAdapter : BaseFadeableAdapter
    {

        [SerializeField]
        private SpriteRenderer fadeableRenderer = null;

        private void Awake()
        {
            IsFadedIn = true;
        }

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
                fadeableRenderer.material.color = new Color(
                    fadeableRenderer.color.r, 
                    fadeableRenderer.color.g, 
                    fadeableRenderer.color.b, 
                    i);
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
            for (float i = fadeableRenderer.color.a; i > 0; i -= OFFSET)
            {
                fadeableRenderer.material.color = new Color(
                    fadeableRenderer.color.r, 
                    fadeableRenderer.color.g, 
                    fadeableRenderer.color.b, 
                    i);
                yield return new WaitForSeconds(delay);
            }
            IsFadedIn = false;
        }

        public override void ResetAlpha()
        {
            fadeableRenderer.material.color = new Color(fadeableRenderer.color.r, fadeableRenderer.color.g, fadeableRenderer.color.b, 1.0f);
            IsFadedIn = true;
        }

    }
}