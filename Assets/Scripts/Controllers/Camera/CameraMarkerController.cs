using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace CameraManagement
{
    public class CameraMarkerController : MonoBehaviour
    {

        private Camera cam;

        [SerializeField]
        AudioSource bgm;

        /// <summary>
        /// On start queue a message of our information then destroy ourselves
        /// </summary>
        private void Start()
        {
            cam = GetComponent<Camera>();
            //Queue a message with our position and rotation then destroy ourselves
            Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                MessageQueueID.CAMERA,
                JsonUtility.ToJson(new CameraMessageObject(transform.position, transform.rotation, true, cam.backgroundColor))
                );

            bgm.transform.parent = null;

            Destroy(gameObject);
        }
    }
}
