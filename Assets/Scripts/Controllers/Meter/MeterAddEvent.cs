using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace UI.Meter
{
    [RequireComponent(typeof(QueueMessageEvent))]
    public class MeterAddEvent : MonoBehaviour
    {
        [SerializeField]
        private int addMeter1Value = 0;

        [SerializeField]
        private int addMeter2Value = 0;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<QueueMessageEvent>().AddMessage(
                MessageQueueID.METER,
                JsonUtility.ToJson(new MeterMessageObject(
                    addMeter1Value,
                    addMeter2Value)
                ));
        }

    }
}
