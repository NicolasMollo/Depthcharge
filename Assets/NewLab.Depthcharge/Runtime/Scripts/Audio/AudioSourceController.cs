using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace Depthcharge.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceController : MonoBehaviour
    {

        private AudioSource source = null;
        private AudioClipData currentAudioClip = null;

        [SerializeField]
        [Tooltip("If you want set the source's audio mixer group")]
        private AudioMixerGroup mixerGroup = null;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float volume = 1.0f;
        [SerializeField]
        private bool playOnAwake = false;
        [SerializeField]
        private bool playInLoop = false;
        [SerializeField]
        private List<AudioClipData> audioClips = null;


        private void Awake()
        {
            source = GetComponent<AudioSource>();
            source.outputAudioMixerGroup = mixerGroup;
            source.volume = volume;
            source.playOnAwake = playOnAwake;
            source.loop = playInLoop;
            string message = $"=== {name}.AudioSourceController.Awake() === Be ensure to add at least an \"AudioClipData\" to \"audioClips\" list!";
            Assert.IsTrue(audioClips.Count > 0, message);
            currentAudioClip = audioClips[0];
        }

        /// <summary>
        /// Play current audio clip.
        /// By default, current audio clip is the first element of list "audioClips".
        /// <returns> Return current audio clip data. </returns>
        /// If you want to set current audio clip you can be use <seealso cref="SetCurrentClip(AudioClipType)"/>.
        /// </summary>
        /// <returns></returns>
        public AudioClipData PlayCurrentClip()
        {
            source.clip = currentAudioClip.AudioClip;
            source.Play();
            return currentAudioClip;
        }
        public void Play(AudioClipType audio)
        {
            AudioClipData clipData = GetFirstClipOfType(audio);
            source.clip = clipData.AudioClip;
            currentAudioClip = clipData;
            source.Play();
        }
        public void Pause()
        {
            source.Pause();
        }
        public void Stop()
        {
            source.Stop();
        }

        /// <summary>
        /// Play one shot current audio clip.
        /// By default, current audio clip is the first element of list "audioClips".
        /// <returns> Return current audio clip data. </returns>
        /// If you want to set current audio clip you can be use <seealso cref="SetCurrentClip(AudioClipType)"/>.
        /// </summary>
        /// <returns></returns>
        public AudioClipData PlayOneShotCurrentClip()
        {
            source.PlayOneShot(currentAudioClip.AudioClip);
            return currentAudioClip;
        }
        public void PlayOneShot(AudioClipType audio)
        {
            AudioClipData clipData = GetFirstClipOfType(audio);
            currentAudioClip = clipData;
            source.PlayOneShot(currentAudioClip.AudioClip);
        }
        public void PlayOneShotRandomClipOfType(AudioClipType type)
        {
            List<AudioClipData> clips = GetAllClipsOftype(type);
            int randomIndex = Random.Range(0, clips.Count);
            currentAudioClip = clips[randomIndex];
            source.PlayOneShot(currentAudioClip.AudioClip);
        }

        public void SetCurrentClip(AudioClipType audio)
        {
            currentAudioClip = GetFirstClipOfType(audio);
        }
        public void SetVolume(float volume)
        {
            source.volume = volume;
        }
        public void SetVolume(float volume, float delay)
        {
            if (source.volume > volume)
            {
                StartCoroutine(SubtractVolume(volume, delay));
            }
            else
            {
                StartCoroutine(AddVolume(volume, delay));
            }
        }

        private IEnumerator SubtractVolume(float volumeTarget, float delay)
        {
            float volumesDiff = source.volume - volumeTarget;
            const float VALUE_TO_COMPARE = 0.05f;
            const float SUBTRAHEND = 0.01f;
            while (volumesDiff >= VALUE_TO_COMPARE)
            {
                source.volume -= SUBTRAHEND;
                yield return new WaitForSeconds(delay);
            }
        }
        private IEnumerator AddVolume(float volumeTarget, float delay)
        {
            float volumesDiff = Mathf.Abs(source.volume - volumeTarget);
            const float VALUE_TO_COMPARE = 0.05f;
            const float ADDEND = 0.01f;
            while (volumesDiff <= VALUE_TO_COMPARE)
            {
                source.volume += ADDEND;
                yield return new WaitForSeconds(delay);
            }
        }

        private AudioClipData GetFirstClipOfType(AudioClipType type)
        {
            AudioClipData audioClipData = null;
            foreach (AudioClipData clip in audioClips)
            {
                if (clip.AudioType == type)
                {
                    audioClipData = clip;
                    return audioClipData;
                }
            }
            string message = $"=== {name}.AudioSourceController.GetAudioClipData() === Audio of type {type.ToString()} is not in this AudioSourceController!";
            Assert.IsNotNull(audioClipData, message);
            return null;
        }
        private List<AudioClipData> GetAllClipsOftype(AudioClipType type)
        {
            List<AudioClipData> clips = new List<AudioClipData>();
            foreach (AudioClipData clip in audioClips)
            {
                if (clip.AudioType == type)
                {
                    clips.Add(clip);
                }
            }
            string message = $"=== {name}.AudioSourceController.GetAudioClipData() === AudioSourceController doesn't contains any \"{type.ToString()}\" audio type";
            Assert.IsTrue(clips.Count > 0, message);
            return clips;
        }

    }

}