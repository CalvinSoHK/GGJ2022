using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.MessageQueue
{
    public class MessageQueue
    {
        //Message queue
        private Queue<string> messageQueue = new Queue<string>();

        /// <summary>
        /// Tries to queue given message
        /// </summary>
        /// <param name="msg"></param>
        public void TryQueueMessage(string msg)
        {
            messageQueue.Enqueue(msg);
        }

        /// <summary>
        /// Tells us if this queue is empty
        /// </summary>
        /// <returns></returns>
        public bool IsQueueEmpty()
        {
            if(messageQueue.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to pop the queue.
        /// If it is able to, it will return true, and output the message to message.
        /// Otherwise it will return false and empty the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryPopQueue(out string message)
        {
            if (!IsQueueEmpty())
            {
                message = messageQueue.Dequeue();
                return true;
            }
            message = "";
            return false;
        }

    }
}