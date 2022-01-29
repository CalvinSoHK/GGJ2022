using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace CameraManagement
{
    /// <summary>
    /// At run time adds a message event with the JSON to cause a camera movement
    /// </summary>
    public class CameraMessageEvent : MonoBehaviour
    {
        [Tooltip("Instead of setting values you can pass in a transform")]
        [SerializeField]
        private Transform targetTransform = null;

        [Tooltip("Target camera position when message fires.")]
        [SerializeField]
        private Vector3 cameraPos = Vector3.zero;

        [Tooltip("Target camera rotation when message fires.")]
        [SerializeField]
        private Vector3 cameraRot = Vector3.zero;

        [Tooltip("When set to true it will add it by default to the QueueMessageEvent")]
        [SerializeField]
        private bool AddToQueueMessageEvent = true;

        private void Start()
        {
            if (AddToQueueMessageEvent)
            {
                GetComponent<QueueMessageEvent>().AddMessage(
                    MessageQueueID.CAMERA,
                    JsonUtility.ToJson(new CameraMessageObject(
                        targetTransform != null ? targetTransform.position : cameraPos,
                        targetTransform != null ? targetTransform.rotation : Quaternion.Euler(cameraRot)
                    )));
            }
        }

        /// <summary>
        /// Lets you manually queue this message
        /// </summary>
        public void QueueMessage()
        {
           Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.CAMERA,
                    JsonUtility.ToJson(new CameraMessageObject(
                        targetTransform != null ? targetTransform.position : cameraPos,
                        targetTransform != null ? targetTransform.rotation : Quaternion.Euler(cameraRot)
                    )));
        }
    }
}
