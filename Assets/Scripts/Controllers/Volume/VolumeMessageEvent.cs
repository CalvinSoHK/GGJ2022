using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Utility.MessageQueue;

namespace VolumeManagement
{
    [RequireComponent(typeof(QueueMessageEvent))]
    public class VolumeMessageEvent : MonoBehaviour
    {
        [Tooltip("Target volume profile we want to use")]
        [SerializeField]
        private string targetProfilePath;

        [Tooltip("Target weight we want to approach")]
        [SerializeField]
        private float targetWeight;

        [Tooltip("Smooth time to get to target weight.")]
        [SerializeField]
        private float smoothTime;

        private void Start()
        {
            GetComponent<QueueMessageEvent>().AddMessage(
                MessageQueueID.POSTPROCESS,
                JsonUtility.ToJson(new VolumeMessageObject(
                    targetProfilePath,
                    targetWeight,
                    smoothTime)
                ));
        }
    }
}
