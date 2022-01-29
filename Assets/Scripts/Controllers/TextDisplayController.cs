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
        private GameObject textNextButton;

        [SerializeField]
        private TextMeshProUGUI textMesh;

        /// <summary>
        /// Text to display queue
        /// </summary>
        private Queue<string> textQueue = new Queue<string>();

        private bool animatingText = false; // displaying the letters in text
        private string fullText = ""; // reference to the complete string to be displayed
        private string currentText = ""; //reference that holds letters from full text
        private int currentTextIndex = 0; //index of which char from fullText to display next
        private float letterTimer = 0f; // current time between letters

        [SerializeField] //amount of time between each letter
        private float letterTimerMax = 0.05f;

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
                textNextButton.SetActive(false);
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

                animatingText = true;
                fullText = textQueue.Dequeue();
                currentText = "";
                currentTextIndex = 0;
                letterTimer = 0;
                textMesh.text = "";

                //textMesh.text = textQueue.Dequeue();
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    "MouseInactive"
                    );
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
                animatingText = true;
                fullText = textQueue.Dequeue();
                currentText = "";
                currentTextIndex = 0;
                letterTimer = 0;
                textMesh.text = "";
                //textMesh.text = textQueue.Dequeue();
            }
            else
            {
                textDisplayWindow.SetActive(false);
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    "MouseActive"
                    );
            }
        }

        void Update()
        {
            ProgressivellyShowText();
        }

        /// <summary>
        /// Displays one letter at a time the last message to be dequeued
        /// </summary>
        private void ProgressivellyShowText()
        {
            if (animatingText)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShowFullText();
                }
                else
                {
                    letterTimer += Time.deltaTime;
                    if (letterTimer >= letterTimerMax)
                    {
                        DisplayNextLetter();
                        letterTimer = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// adds next letter from the message
        /// </summary>
        private void DisplayNextLetter()
        {
            if (currentTextIndex < fullText.Length)
            {
                currentText += fullText[currentTextIndex];
                textMesh.text = currentText;
                currentTextIndex += 1;
            }
            else
            {
                ShowFullText();
            }
            
        }

        /// <summary>
        /// shows the message in full and activates the next button
        /// </summary>
        private void ShowFullText()
        {
            textMesh.text = fullText;
            animatingText = false;
            textNextButton.SetActive(true);
        }

    }
}