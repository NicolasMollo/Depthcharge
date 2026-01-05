using System;
using UnityEngine;
using UnityEngine.Assertions;


namespace Depthcharge.TimeManagement
{
    public class TimeSystem : MonoBehaviour
    {

        private float currentTimeScale = 0f;
        public Action<float> OnSetTimeScale = null;

        private void Awake()
        {
            currentTimeScale = Time.timeScale;
        }

        public void SetTimeScale(float timeScale)
        {
            if (timeScale == currentTimeScale)
            {
                string message = $"=== {GetType().ToString()}.SetTimeScale() === You have try to set time scale with the same value!";
                Assert.IsTrue(false, message);
            }
            Time.timeScale = timeScale;
            currentTimeScale = Time.timeScale;
            OnSetTimeScale?.Invoke(currentTimeScale);
        }

    }

}