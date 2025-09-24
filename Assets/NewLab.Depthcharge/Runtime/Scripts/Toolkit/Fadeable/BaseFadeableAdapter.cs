using System.Collections;
using UnityEngine;

namespace Depthcharge.Toolkit
{

    public abstract class BaseFadeableAdapter : MonoBehaviour
    {
        public bool IsFadedIn { protected set; get; }
        public abstract void FadeIn(float delay, float treshold);
        protected abstract IEnumerator InternalFadeIn(float delay, float treshold);
        public abstract void FadeOut(float delay);
        protected abstract IEnumerator InternalFadeOut(float delay);

    }
}