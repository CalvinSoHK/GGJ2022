using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utility.MessageQueue;

namespace UI.Meter
{
    public class MeterController : MonoBehaviour
    {
        private const int METER1_MAX = 10;
        private const int METER2_MAX = 10;

        /// <summary>
        /// Current meter value
        /// </summary>
        private float cur_meter1 = 0;
        private float cur_meter2 = 0;

        /// <summary>
        /// Target meter values for animation
        /// </summary>
        private int target_meter1 = 0;
        private int target_meter2 = 0;

        /// <summary>
        /// Ref for smoothdamp on meters
        /// </summary>
        private float refMeter1 = 0f;
        private float refMeter2 = 0f;

        [SerializeField]
        Image meter1;

        [SerializeField]
        Image meter2;

        [Range(0, 1)]
        [Tooltip("Smooth damp time for meters filling")]
        [SerializeField]
        private float smoothDampTime = 0.2f;

        private bool breakCoroutine = false;

        public static string UPDATE_METER = "UpdateMeter";

        public static string METER_COMPLETE = "CompleteMeter";

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
                if (msg.Equals(UPDATE_METER))
                {
                    UpdateMeters();
                }
            }
        }

        /// <summary>
        /// Updates meters visually
        /// </summary>
        private async void UpdateMeters()
        {
            breakCoroutine = true;
            await Task.Delay((int)(Time.deltaTime * 1000));
            breakCoroutine = false;

            while (!breakCoroutine)
            {
                cur_meter1 = Mathf.SmoothDamp(cur_meter1, target_meter1, ref refMeter1, smoothDampTime);
                cur_meter2 = Mathf.SmoothDamp(cur_meter2, target_meter2, ref refMeter2, smoothDampTime);

                meter1.fillAmount = cur_meter1 / METER1_MAX;
                meter2.fillAmount = cur_meter2 / METER2_MAX;

                float distance = (Mathf.Abs(cur_meter1 - target_meter1) + Mathf.Abs(cur_meter2 - target_meter2))/ 2;
                if(distance <= 0.01f)
                {
                    break;
                }
                
                await Task.Delay((int)(Time.deltaTime * 1000));
            }

            //When we've exited we are close enough to just go to match the value
            cur_meter1 = target_meter1;
            cur_meter2 = target_meter2;
            meter1.fillAmount = cur_meter1 / METER1_MAX;
            meter2.fillAmount = cur_meter2 / METER2_MAX;

            //Now queue a message for being done updating the meter
            Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                MessageQueueID.UI,
                METER_COMPLETE
                );
        }

        /// <summary>
        /// Updates the values
        /// </summary>
        /// <param name="_targetMeter1"></param>
        /// <param name="_targetMeter2"></param>
        public void UpdateValues(int _targetMeter1, int _targetMeter2)
        {
            target_meter1 = _targetMeter1;
            target_meter2 = _targetMeter2;
        }
    }
}
