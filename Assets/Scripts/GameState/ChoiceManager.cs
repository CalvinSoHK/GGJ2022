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

        public static string CHOICE1_MSG = "Choice1";
        public static string CHOICE2_MSG = "Choice2";

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
            if (id.Equals(MessageQueueID.UI))
            {
                if (msg.Equals(CHOICE1_MSG))
                {
                    choice1Count++;
                }
                else if(msg.Equals(CHOICE2_MSG))
                {
                    choice2Count++;
                }
            }
        }
    }
}
