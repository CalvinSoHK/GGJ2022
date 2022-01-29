using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Utility.MessageQueue;
using UnityEngine.Events;

namespace VolumeManagement
{
    /// <summary>
    /// Controls the volume profile values
    /// Post Processing volume! Not audio!
    /// </summary>
    [RequireComponent(typeof(Volume))]
    public class VolumeController : MonoBehaviour
    {
        /// <summary>
        /// The profile we want to activate when we get the message to
        /// </summary>
        [SerializeField]
        private Volume targetVolume;

        /// <summary>
        /// Needed to smoothdamp
        /// </summary>
        private float refVelocity;

        /// <summary>
        /// Used in smooth damp to weight values
        /// </summary>
        private float smoothTime = 0.2f;

        private UnityEvent endEvent;

        /// <summary>
        /// Target weight we want to be.
        /// </summary>
        private float targetWeight = 1;




        /// <summary>
        /// Breaks existing coroutines when set to true.
        /// </summary>
        private bool breakCoroutine = false;

        /// <summary>
        /// Message that tells us to reset volume (set add volume to 0);
        /// </summary>
        public static string RESET_MESSAGE = "ResetVolume";

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
            if (id.Equals(MessageQueueID.UI))
            {
                if (msg.Equals(RESET_MESSAGE))
                {
                    ResetVolume();
                }
            }
            else if (id.Equals(MessageQueueID.POSTPROCESS))
            {
                ProcessMessage(msg);
            }
        }

        /// <summary>
        /// Resets volume by setting the additional volume back to 0.
        /// </summary>
        private void ResetVolume()
        {
            targetWeight = 0;
            StartCoroutine(ApproachWeight());
        }

        /// <summary>
        /// Processes the message to and begins smoothdamping to the volume weight
        /// </summary>
        /// <param name="msg"></param>
        private void ProcessMessage(string msg)
        {
            VolumeMessageObject obj = JsonUtility.FromJson<VolumeMessageObject>(msg);
            smoothTime = obj.SmoothTime;
            targetWeight = obj.TargetWeight;
            VolumeProfile targetProfile = Resources.Load<VolumeProfile>(obj.VolumeProfilePath);
            targetVolume.profile = targetProfile;
            endEvent = obj.EndEvent;
            StartCoroutine(ApproachWeight());
        }

        /// <summary>
        /// Smoothdamps towards the target weight
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApproachWeight()
        {
            //Break out of other existing coroutine
            breakCoroutine = true;
            yield return new WaitForEndOfFrame();
            breakCoroutine = false;

            //Move to target position until broken or we reach the position
            while (!breakCoroutine)
            {
                targetVolume.weight = Mathf.SmoothDamp(targetVolume.weight, targetWeight, ref refVelocity, smoothTime);
                if(Mathf.Abs(targetVolume.weight - targetWeight) < 0.01f)
                {
                    if(endEvent != null)
                    {
                        endEvent?.Invoke();
                    }
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
