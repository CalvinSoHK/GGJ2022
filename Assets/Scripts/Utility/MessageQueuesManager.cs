using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.MessageQueue
{
    public class MessageQueuesManager : MonoBehaviour
    {
        /// <summary>
        /// Dictionary of message queues
        /// Key is key for the message queue
        /// </summary>
        private Dictionary<string, MessageQueue> messageQueueDict = new Dictionary<string, MessageQueue>();

        public delegate void MessageEvent(string id, string message);

        /// <summary>
        /// Event that has an ID and string message.
        /// Can be listened to by any script.
        /// </summary>
        public static MessageEvent MessagePopEvent;

        /// <summary>
        /// Tries make a new queue with the given id.
        /// Will return true if successful
        /// </summary>
        /// <param name="id"></param>
        public bool TryMakeQueue(string id)
        {
            if(!messageQueueDict.ContainsKey(id))
            {
                messageQueueDict.Add(id, new MessageQueue());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to queue a given message to a queue of id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryQueueMessage(string id, string message)
        {
            if (messageQueueDict.ContainsKey(id))
            {
                MessageQueue queue;
                if(messageQueueDict.TryGetValue(id, out queue))
                {
                    queue.TryQueueMessage(message);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attempts to pop the message queue of a given ID
        /// Returns true for success, false for failure.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TryPopMessageQueue(string id)
        {
            if (messageQueueDict.ContainsKey(id))
            {
                MessageQueue queue;
                if (messageQueueDict.TryGetValue(id, out queue))
                {
                    string message;
                    if(queue.TryPopQueue(out message))
                    {
                        MessagePopEvent?.Invoke(id, message);
                    }
                }
            }
            return false;
        }

        // Start is called before the first frame update
        void Start()
        {
            //Make a queue for each ID in the list
            foreach(string id in MessageQueueID.IDList)
            {
                bool status = TryMakeQueue(id);
                if (!status)
                {
                    throw new System.Exception("MessageQueueManager Error: Attempted to make a queue of an already existing id. ID: " + id);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //Try to pop the message queue for each ID
            foreach (string id in MessageQueueID.IDList)
            {
                TryPopMessageQueue(id);
            }
        }
    }
}
