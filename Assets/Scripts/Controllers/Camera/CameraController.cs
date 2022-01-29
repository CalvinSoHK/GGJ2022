using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Utility.MessageQueue;
using VolumeManagement;
using UnityEngine.Rendering.Universal;

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

        public static string COMPLETE_MESSAGE = "CameraComplete";
        /// <summary>
        /// The profile we want to activate when we get the message to
        /// </summary>
        [SerializeField]
        private Volume targetVolume;

        ColorAdjustments caComponent;


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
            else if(id.Equals(MessageQueueID.POSTPROCESS))
            {
                VolumeMessageObject obj = JsonUtility.FromJson<VolumeMessageObject>(msg);
                VolumeProfile targetProfile = Resources.Load<VolumeProfile>(obj.VolumeProfilePath);
                if(targetProfile != null)
                {
                    //targetVolume.profile = targetProfile;

                    ColorAdjustments tmp;
                    if (targetProfile.TryGet<ColorAdjustments>(out tmp))
                    {
                        caComponent = tmp;
                    }
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
            GetComponent<Camera>().backgroundColor = caComponent != null ? (Color) caComponent.colorFilter : GetComponent<Camera>().backgroundColor;
            //change color here
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

                //Terminate coroutine if we are close enough
                if(Vector3.Distance(transform.position, targetPos) <= 0.01f)
                {
                    //Message that we finished camera move
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.GAMESTATE,
                        COMPLETE_MESSAGE
                        );
                    break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
