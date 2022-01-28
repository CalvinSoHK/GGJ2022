using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.MessageQueue
{
    [System.Serializable]
    public class MessageInfo
    {
        [Tooltip("ID of the queue we want to send the message to.")]
        [SerializeField]
        private string id;

        /// <summary>
        /// ID of the queue we want to send the message through.
        /// </summary>
        public string ID
        {
            get
            {
                return id;
            }
        }

        [Tooltip("Message that we want to put through the queue.")]
        [SerializeField]
        private string message;

        /// <summary>
        /// The message content we need to send
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// Constructor sets both values with given inputs
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_message"></param>
        public MessageInfo(string _id, string _message)
        {
            id = _id;
            message = _message;
        }
    }
}
