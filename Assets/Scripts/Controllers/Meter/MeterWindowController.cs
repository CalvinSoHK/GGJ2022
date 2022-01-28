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

        public static string SHOW_METER = "ShowMeter";

        public static string HIDE_METER = "HideMeter";

        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }

        private void HandleMessage(string id, string message)
        {
            if (id.Equals(MessageQueueID.UI))
            {
                if (message.Equals(SHOW_METER))
                {
                    meterWindow.SetActive(true);
                }
                else if (message.Equals(HIDE_METER))
                {
                    meterWindow.SetActive(false);
                }
            }
        }
    }
}
