using UnityEngine;
using UnityEngine.UI;
using Utility.MessageQueue;

namespace UI.Meter
{
    public class MeterController : MonoBehaviour
    {
        private const int METER1_MAX = 10;
        private const int METER2_MAX = 10;

        private int meter1Value = 0;
        private int meter2Value = 0;

        [SerializeField]
        Image meter1;

        [SerializeField]
        Image meter2;

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
            if (id.Equals(MessageQueueID.METER))
            {
                MeterMessageObject obj = JsonUtility.FromJson<MeterMessageObject>(msg);
                meter1Value += obj.Meter1Value;
                meter2Value += obj.Meter2Value;
                UpdateMeters();
            }
        }

        /// <summary>
        /// Updates meters visually
        /// </summary>
        private void UpdateMeters()
        {
            meter1.fillAmount = (float)meter1Value / (float)METER1_MAX;
            meter2.fillAmount = (float)meter2Value / (float)METER2_MAX;
        }
    }
}
