using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace Depthcharge.Audio
{

    [DisallowMultipleComponent]
    public class AudioSystem : MonoBehaviour
    {

        [SerializeField]
        private AudioMixer audioMixer = null;
        [SerializeField]
        private AudioSourceController sourceUI = null;

        private const string MUSIC_VOLUME_NAME = "MusicVolume";
        private const string GAMESFX_VOLUME_NAME = "GameSfxVolume";
        private const string UISFX_VOLUME_NAME = "UISfxVolume";


        private void Awake()
        {
            string message = $"=== AudioSystem.Awake() === Be ensure to fill \"audioMixer\" field in Inspector!";
            Assert.IsNotNull(audioMixer, message);
            message = $"=== AudioSystem.Awake() === Be ensure to fill \"sourceUI\" field in Inspector!";
            Assert.IsNotNull(sourceUI, message);
        }

        public void SetMusicVolume(float volume)
        {
            audioMixer.SetFloat(MUSIC_VOLUME_NAME, AudioHelper.VolumeToDecibels(volume));
        }
        public void SetSfxVolumes(float volume)
        {
            audioMixer.SetFloat(GAMESFX_VOLUME_NAME, AudioHelper.VolumeToDecibels(volume));
            audioMixer.SetFloat(UISFX_VOLUME_NAME, AudioHelper.VolumeToDecibels(volume));
        }
        public void SetUISfxVolumes(float volume)
        {
            audioMixer.SetFloat(UISFX_VOLUME_NAME, AudioHelper.VolumeToDecibels(volume));
        }
        public void SetGameSfxVolumes(float volume)
        {
            audioMixer.SetFloat(GAMESFX_VOLUME_NAME, AudioHelper.VolumeToDecibels(volume));
        }

        public void PlayHoverSfx()
        {
            sourceUI.PlayOneShot(AudioClipType.UI_Hover);
        }
        public void PlayConfirmSfx()
        {
            sourceUI.PlayOneShot(AudioClipType.UI_Confirm);
        }
        public void PlayCancelSfx()
        {
            sourceUI.PlayOneShot(AudioClipType.UI_Cancel);
        }
        public void PlayScorePoints()
        {
            sourceUI.PlayOneShot(AudioClipType.UI_ScorePoint);
        }
        public void PlayTimePoints()
        {
            sourceUI.PlayOneShot(AudioClipType.UI_TimePoint);
        }
        public void PlayKeyPress()
        {
            sourceUI.PlayOneShotRandomClipOfType(AudioClipType.UI_KeyPress);
        }

    }

}