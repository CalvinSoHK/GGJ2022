using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;
using UnityEngine.Events;

namespace VolumeManagement
{
    public class VolumeMessageObject : MessageObject
    {
        [SerializeField]
        private string volumeProfilePath;

        /// <summary>
        /// Path of the volume profile we want to transition to/away from.
        /// </summary>
        public string VolumeProfilePath
        {
            get
            {
                return volumeProfilePath;
            }
        }

        [SerializeField]
        private float targetWeight;

        /// <summary>
        /// Target weight we want to reach
        /// </summary>
        public float TargetWeight
        {
            get
            {
                return targetWeight;
            }
        }

        [SerializeField]
        private float smoothTime;

        /// <summary>
        /// The smooth time we want to use to get to the target weight
        /// </summary>
        public float SmoothTime
        {
            get
            {
                return smoothTime;
            }
        }

        [SerializeField]
        private UnityEvent endEvent;
        /// <summary>
        /// Event to invoke when we reach the target Event the target weight
        /// </summary>
        public UnityEvent EndEvent
        {
            get
            {
                return endEvent;
            }
        }

        public VolumeMessageObject(string _path, float _weight, float _time, UnityEvent _event )
        {
            volumeProfilePath = _path;
            targetWeight = _weight;
            smoothTime = _time;
            endEvent = _event;

        }
    }
}
