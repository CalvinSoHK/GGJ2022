using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace UI.Meter
{
    public class MeterWindowController : MonoBehaviour
    {
        [SerializeField]
        private GameObject meterWindow;

        private MeterController meterController;

        public static string SHOW_METER = "ShowMeter";

        public static string HIDE_METER = "HideMeter";

        /// <summary>
        /// Current meter value
        /// </summary>
        private int cur_meter1 = 0;
        private int cur_meter2 = 0;


        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }

        private void Start()
        {
            meterController = meterWindow.GetComponent<MeterController>();
        }

        private void HandleMessage(string id, string message)
        {
            if (id.Equals(MessageQueueID.UI))
            {
                if (message.Equals(SHOW_METER))
                {
                    meterWindow.SetActive(true);
                    meterController.UpdateValues(cur_meter1, cur_meter2);
                }
                else if (message.Equals(HIDE_METER))
                {
                    meterWindow.SetActive(false);
                }               
            }
            else if (id.Equals(MessageQueueID.METER))
            {
                MeterMessageObject obj = JsonUtility.FromJson<MeterMessageObject>(message);
                cur_meter1 += obj.Meter1Value;
                cur_meter2 += obj.Meter2Value;
            }
        }
    }
}
