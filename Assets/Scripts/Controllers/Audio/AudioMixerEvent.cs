using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioManagement
{
    public class AudioMixerEvent : MonoBehaviour
    {
        public delegate void AudioMixerDelegate(AudioMixerMessageObject obj);
        public static AudioMixerDelegate TransitionAudioMixerEvent;

        [SerializeField]
        private AudioMixerSnapshot targetSnapshot;

        [SerializeField]
        private float transitionTime;

        public void TransitionMixer()
        {
            TransitionAudioMixerEvent?.Invoke(new AudioMixerMessageObject(
                targetSnapshot,
                transitionTime
                ));
        }
    }
}
