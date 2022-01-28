using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility.MessageQueue;

namespace UI.TextDisplay
{
    public class TextDisplayController : MonoBehaviour
    {
        [SerializeField]
        private GameObject textDisplayWindow;

        [SerializeField]
        private TextMeshProUGUI textMesh;

        /// <summary>
        /// Text to display queue
        /// </summary>
        private Queue<string> textQueue = new Queue<string>();

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
            if (id.Equals(MessageQueueID.DIALOGUE))
            {
                ProcessAddMessage(message);
            }
            else if (id.Equals(MessageQueueID.UI))
            {
                if (message.Equals("ContinueText"))
                {
                    ProcessNext();
                }
            }
        }

        /// <summary>
        /// Processes when we receive a new message.
        /// If we are not yet currently displaying anything, we will begin displaying text.
        /// </summary>
        private void ProcessAddMessage(string message)
        {
            textQueue.Enqueue(message);
            if (!textDisplayWindow.activeSelf && textQueue.Count > 0)
            {
                textMesh.text = textQueue.Dequeue();
                textDisplayWindow.SetActive(true);
            }
        }

        /// <summary>
        /// Process the Continue button being clicked.
        /// If there are still elements in the queue
        /// </summary>
        private void ProcessNext()
        {
            //If we can still dequeue new content, do so and set the text
            if (textQueue.Count > 0)
            {
                textMesh.text = textQueue.Dequeue();
            }
            else
            {
                textDisplayWindow.SetActive(false);
            }
        }

    }
}