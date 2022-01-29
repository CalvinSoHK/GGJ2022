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
    [RequireComponent(typeof(QueueMessageEvent))]
    public class CameraMessageEvent : MonoBehaviour
    {
        [Tooltip("Target camera position when message fires.")]
        [SerializeField]
        private Vector3 cameraPos = Vector3.zero;

        [Tooltip("Target camera rotation when message fires.")]
        [SerializeField]
        private Vector3 cameraRot = Vector3.zero;

        private void Start()
        {
            GetComponent<QueueMessageEvent>().AddMessage(
                MessageQueueID.CAMERA,
                JsonUtility.ToJson(new CameraMessageObject(
                    cameraPos,
                    Quaternion.Euler(cameraRot))
                ));
        }
    }
}
