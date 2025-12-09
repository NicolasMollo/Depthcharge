using UnityEngine;

namespace Depthcharge.Audio
{
    internal class AudioHelper
    {

        internal static float VolumeToDecibels(float volume)
        {
            if (volume <= 0.0001f)
            {
                return -80f;
            }

            return Mathf.Log10(volume) * 20f;
        }
        internal static float DecibelsToVolume(float dB)
        {
            return Mathf.Pow(10f, dB / 20f);
        }

    }

}