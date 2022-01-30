using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioManagement
{
    public class AudioPlayEvent : MonoBehaviour
    {
        public delegate void PlayAudioDelegate(AudioClipMessageObject obj);
        public static PlayAudioDelegate PlayAudioEvent;

        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        private AudioMixerGroup audioMixerGroup;

        public void PlayAudio()
        {
            PlayAudioEvent?.Invoke(new AudioClipMessageObject(
                audioClip,
                audioMixerGroup
                ));
        }
    }
}
