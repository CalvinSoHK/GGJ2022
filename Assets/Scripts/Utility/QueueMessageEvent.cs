using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace Event
{
    public class QueueMessageEvent : MonoBehaviour
    {
        [Tooltip("ID of the queue we want to send the message to.")]
        [SerializeField]
        private string id;

        [Tooltip("Message that we want to put through the queue.")]
        [SerializeField]
        private string message;

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

            manager.TryQueueMessage(id, message);
        }
    }
}
