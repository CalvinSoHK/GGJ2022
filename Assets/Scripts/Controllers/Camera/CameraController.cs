using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace CameraManagement
{
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// Origin for camera currently. Returns to this when told to.
        /// </summary>
        private Vector3 originPos = Vector3.zero;

        /// <summary>
        /// Origin rotation for camera currently. Returns to this when told to.
        /// </summary>
        private Quaternion originRot = Quaternion.identity;

        private Vector3 targetPos = Vector3.zero;
        private Quaternion targetRot = Quaternion.identity;

        private Vector3 refPosVelocity = Vector3.zero;
        private float refRotVelocity = 0f;

        [SerializeField]
        private float positionSmoothTime = 0.2f;

        [SerializeField]
        private float rotationSmoothTime = 0.2f;

        /// <summary>
        /// Breaks existing coroutines when set to true.
        /// </summary>
        private bool breakCoroutine = false;

        public static string RESET_MESSAGE = "ResetCamera";

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
                if (obj.IsOrigin)
                {
                    HandleNewOrigin(obj);
                }
                else
                {
                    HandleCameraPoint(obj);
                }
            }
            else if (id.Equals(MessageQueueID.UI))
            {
                if (msg.Equals(RESET_MESSAGE))
                {
                    HandleCameraReset();
                }
            }
        }

        /// <summary>
        /// Handles when we are given a new origin
        /// </summary>
        /// <param name="obj"></param>
        private void HandleNewOrigin(CameraMessageObject obj)
        {
            originPos = obj.Position;
            originRot = obj.Rotation;
            transform.SetPositionAndRotation(obj.Position, obj.Rotation);
        }

        /// <summary>
        /// Handles when we are given a new position and rotation to move to
        /// </summary>
        /// <param name="obj"></param>
        private void HandleCameraPoint(CameraMessageObject obj)
        {
            targetPos = obj.Position;
            targetRot = obj.Rotation;
            StartCoroutine(LerpCamera());
        }

        /// <summary>
        /// Handles when we call for the camera to reset
        /// </summary>
        private void HandleCameraReset()
        {
            targetPos = originPos;
            targetRot = originRot;
            StartCoroutine(LerpCamera());
        }

        /// <summary>
        /// Lerps camera to target
        /// </summary>
        /// <returns></returns>
        private IEnumerator LerpCamera()
        {
            //Break out of other existing coroutine
            breakCoroutine = true;
            yield return new WaitForEndOfFrame();
            breakCoroutine = false;

            //Move to target position until broken or we reach the position
            while (!breakCoroutine)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refPosVelocity, positionSmoothTime);

                float delta = Quaternion.Angle(transform.rotation, targetRot);
                if (delta > 0f)
                {
                    float t = Mathf.SmoothDampAngle(delta, 0.0f, ref refRotVelocity, rotationSmoothTime);
                    t = 1.0f - (t / delta);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, t);
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
