using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace CameraManagement
{
    public class CameraController : MonoBehaviour
    {
        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }

        private void HandleMessage(string id, string msg)
        {
            if (id.Equals(MessageQueueID.CAMERA))
            {
                CameraMessageObject obj = JsonUtility.FromJson<CameraMessageObject>(msg);
                transform.SetPositionAndRotation(obj.Position, obj.Rotation);
            }
        }
    }
}
