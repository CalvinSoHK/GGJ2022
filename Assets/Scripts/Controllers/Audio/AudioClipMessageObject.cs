using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utility.MessageQueue;

namespace AudioManagement
{
    /// <summary>
    /// Cannot be JSONified if we want references to the audio and mixer group.
    /// </summary>
    public class AudioClipMessageObject : MessageObject
    {
        [SerializeField]
        private AudioClip targetClip;

        /// <summary>
        /// Target clip to play
        /// </summary>
        public AudioClip TargetClip
        {
            get
            {
                return targetClip;
            }
        }

        [SerializeField]
        private AudioMixerGroup targetGroup;

        /// <summary>
        /// Target group to play
        /// </summary>
        public AudioMixerGroup TargetGroup
        {
            get
            {
                return targetGroup;
            }
        }

        public AudioClipMessageObject(AudioClip _clip, AudioMixerGroup _group)
        {
            targetClip = _clip;
            targetGroup = _group;
        }

    }
}
