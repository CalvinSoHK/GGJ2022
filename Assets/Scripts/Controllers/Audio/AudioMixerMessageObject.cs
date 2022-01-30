using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utility.MessageQueue;

namespace AudioManagement
{
    //Can't deserialize, not a real message objcet. Should probably be a DTO
    public class AudioMixerMessageObject : MessageObject
    {
        [SerializeField]
        private AudioMixerSnapshot targetSnapshot;

        /// <summary>
        /// The target snapshot we want to switch to
        /// </summary>
        public AudioMixerSnapshot TargetSnapshot
        {
            get
            {
                return targetSnapshot;
            }
        }

        [SerializeField]
        private float transitionTime;

        /// <summary>
        /// Time to transition to this snapshot
        /// </summary>
        public float TransitionTime
        {
            get
            {
                return transitionTime;
            }
        }

        public AudioMixerMessageObject(AudioMixerSnapshot _targetSnapshot, float _transitionTime)
        {
            targetSnapshot = _targetSnapshot;
            transitionTime = _transitionTime;
        }
    }
}
