using CameraManagement;
using ChoiceManagement;
using Player;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.MessageQueue;
using VolumeManagement;

namespace UI.TextDisplay
{
    public class TextDisplayController : MonoBehaviour
    {
        [SerializeField]
        private GameObject textDisplayWindow;

        [Tooltip("The actual next button that we need to set active")]
        [SerializeField]
        private GameObject textNextButton;

        [Tooltip("The actual dialogue box as a button.")]
        [SerializeField]
        private Button dialogueBoxButton;

        [SerializeField]
        private TextMeshProUGUI textMesh;

        [Tooltip("Wait time after clicking before allowing you to click again to proceed through dialogue")]
        [Range(0f, 10f)]
        [SerializeField]
        private float inputDelayBySeconds = 2;

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
                
                if (message.Equals("ContinueText"))
                {
                    textNextButton.SetActive(false);
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
                dialogueBoxButton.interactable = false;

                //textMesh.text = textQueue.Dequeue();
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    InvestigationManager.MOUSE_INACTIVE_MESSAGE
                    );

                //Turn the choice window back on
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    ChoiceWindowController.HIDE_CHOICE
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
                dialogueBoxButton.interactable = false;
                //textMesh.text = textQueue.Dequeue();
            }
            else
            {
                textDisplayWindow.SetActive(false);
                dialogueBoxButton.GetComponent<ButtonController>().ResetSprite();

                //Activate mouse investigation
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    InvestigationManager.MOUSE_ACTIVE_MESSAGE
                    );

                //Reset the camera's position
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    CameraController.RESET_MESSAGE
                    );

                //Reset the post process add volume
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    VolumeController.RESET_MESSAGE
                    );

                //Turn the choice window back on
                Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                    MessageQueueID.UI,
                    ChoiceWindowController.SHOW_CHOICE
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
                    WaitForFramesThenInteractable(inputDelayBySeconds);
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
                dialogueBoxButton.interactable = true;
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

        private async void WaitForFramesThenInteractable(float numSeconds)
        {
            await Task.Delay((int)(numSeconds * 1000f));

            if(dialogueBoxButton != null && !dialogueBoxButton.interactable)
            {
                dialogueBoxButton.interactable = true;
            }
        }
    }
}