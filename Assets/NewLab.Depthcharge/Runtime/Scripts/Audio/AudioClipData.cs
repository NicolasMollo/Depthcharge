using UnityEngine;

namespace Depthcharge.Audio
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Audio/AudioClipData")]
    public class AudioClipData : ScriptableObject
    {

        [SerializeField]
        private AudioClip audioClip = null;
        [SerializeField]
        private AudioClipType audioType = AudioClipType.Shoot;

        internal AudioClip AudioClip
        {
            get => audioClip;
        }
        internal AudioClipType AudioType
        {
            get => audioType;
        }

    }


}