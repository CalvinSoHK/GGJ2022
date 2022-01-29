using CameraManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace ChoiceManagement
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField]
        private int choice1Count = 0;

        [SerializeField]
        private int choice2Count = 0;

        public static string HOVER_LEFT = "Hover/Left";
        public static string DEHOVER_LEFT = "Dehover/Left";

        public static string HOVER_RIGHT = "Hover/Right";
        public static string DEHOVER_RIGHT = "Dehover/Right";

        public static string CHOOSE_LEFT = "Choose/Left";
        public static string CHOOSE_RIGHT = "Choose/Right";

        private bool chooseActive = false;

        /// <summary>
        /// The object we are currently set to
        /// </summary>
        private ChoiceMessageObject curObject = null;

        private enum WindowSide
        {
            None = 0,
            Left = 1,
            Right = 2
        }

        /// <summary>
        /// Which side our mouse is currently on
        /// </summary>
        private WindowSide side = WindowSide.None;

        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }

        void Update()
        {
            if (chooseActive)
            {
                ProcessMouseSide();
                ProcessMouseClick();
            }
        }

        /// <summary>
        /// Processes the mouse moving sides and queues a message when it switches
        /// </summary>
        private void ProcessMouseSide()
        {
            //Use mouse position and queue a message if we change sides
            Vector3 mousePos = Input.mousePosition;
            if(mousePos.x < Screen.width / 2)
            {
                if(side != WindowSide.Left)
                {
                    side = WindowSide.Left;
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        HOVER_LEFT
                        );
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        DEHOVER_RIGHT
                        );
                }
            }
            else
            {
                if (side != WindowSide.Right)
                {
                    side = WindowSide.Right;
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        HOVER_RIGHT
                        );
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        DEHOVER_LEFT
                        );
                }
            }      
        }

        /// <summary>
        /// Processes if we get a mouse click and selects a side
        /// </summary>
        private void ProcessMouseClick()
        {
            //When we get a button press, choose one side
            if (Input.GetMouseButtonDown(0))
            {
                if (side == WindowSide.Left)
                {
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        CHOOSE_LEFT
                        );

                    curObject = new ChoiceMessageObject(
                            CharacterEnum.Character1,
                            choice1Count,
                            choice1Count + 1
                            );

                    choice1Count++;
                    MessageQueuesManager.MessagePopEvent += HandleCameraComplete;
                }
                else if (side == WindowSide.Right)
                {
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.UI,
                        CHOOSE_RIGHT
                        );
                    curObject = new ChoiceMessageObject(
                        CharacterEnum.Character2,
                        choice2Count,
                        choice2Count + 1
                        );

                    choice2Count++;
                    MessageQueuesManager.MessagePopEvent += HandleCameraComplete;
                }
                else
                {
                    Debug.LogError("ChoiceManager Error: Somehow selected a None side.");
                }
                chooseActive = false;
            }
        }

        private void HandleCameraComplete(string id, string msg)
        {
            if (id.Equals(MessageQueueID.GAMESTATE))
            {
                if (msg.Equals(CameraController.COMPLETE_MESSAGE))
                {
                    //Remove ourselves after firing once
                    MessageQueuesManager.MessagePopEvent -= HandleCameraComplete;

                    //Check for null
                    if(curObject == null)
                    {
                        throw new System.Exception("ChoiceManager Exception: CurObject not set.");
                    }

                    //Queue message
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.SELECTION,
                        JsonUtility.ToJson(curObject)
                        );

                    //Null curObject
                    curObject = null;
                }
            }

        }

        private void HandleMessage(string id, string msg)
        {
            if (id.Equals(MessageQueueID.GAMESTATE))
            {
                if (msg.Equals("Choose"))
                {
                    chooseActive = true;
                }
                else if (msg.Equals("Investigate"))
                {
                    chooseActive = false;
                }
            }
        }
    }
}
