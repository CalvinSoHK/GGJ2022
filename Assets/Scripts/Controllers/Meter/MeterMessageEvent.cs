using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace UI.Meter
{
    public class MeterMessageEvent : MonoBehaviour
    {
        [SerializeField]
        private int addMeter1Value = 0;

        [SerializeField]
        private int addMeter2Value = 0;

        [SerializeField]
        private bool addOnStart = false;

        // Start is called before the first frame update
        void Start()
        {
            if (addOnStart)
            {
                GetComponent<QueueMessageEvent>().AddMessage(
                    MessageQueueID.METER,
                    JsonUtility.ToJson(new MeterMessageObject(
                        addMeter1Value,
                        addMeter2Value)
                    ));
            }
        }

        public void QueueMessage()
        {
            Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                MessageQueueID.METER,
                JsonUtility.ToJson(new MeterMessageObject(
                    addMeter1Value,
                    addMeter2Value)
                ));
        }

    }
}
