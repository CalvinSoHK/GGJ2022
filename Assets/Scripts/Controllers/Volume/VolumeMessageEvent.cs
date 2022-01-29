using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Utility.MessageQueue;
using UnityEngine.Events;

namespace VolumeManagement
{ 
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

        [Tooltip("Event to be invoked when target weight is reached")]
        [SerializeField]
        private UnityEvent endEvent;

        [SerializeField]
        private bool AddToQueueMessageEvent = true;

        private void Start()
        {
            if (AddToQueueMessageEvent)
            {
                GetComponent<QueueMessageEvent>().AddMessage(
                    MessageQueueID.POSTPROCESS,
                    JsonUtility.ToJson(new VolumeMessageObject(
                        targetProfilePath,
                        targetWeight,
                        smoothTime,
                        endEvent)
                    ));
            }
        }

        public void QueueMessage()
        {
            Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.POSTPROCESS,
                    JsonUtility.ToJson(new VolumeMessageObject(
                    targetProfilePath,
                    targetWeight,
                    smoothTime,
                    endEvent)
                ));
        }
    }
}
