using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace Event
{
    public class QueueMessageEvent : MonoBehaviour
    {
        [SerializeField]
        private List<MessageInfo> messageInfoList = new List<MessageInfo>();

        /// <summary>
        /// Public function to queue the inputted message.
        /// Can be called from other scripts or button events , etc.
        /// </summary>
        public void QueueMessage()
        {
            MessageQueuesManager manager = Singleton.Instance.GetComponent<MessageQueuesManager>();
            if(manager == null)
            {
                throw new System.Exception("QueueMessageEvent Error: No MessageQueuesManager on Singleton instance: " + Singleton.Instance.name);
            }

            foreach(MessageInfo info in messageInfoList)
            {
                manager.TryQueueMessage(info.ID, info.Message);
            }
        }
    }
}
