using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace ChoiceManagement
{
    public class ChoiceWindowController : MonoBehaviour
    {
        [SerializeField]
        GameObject choiceWindow;

        public static string SHOW_CHOICE = "ShowChoice";

        public static string HIDE_CHOICE = "HideChoice";

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
            if (id.Equals(MessageQueueID.UI))
            {
                if (message.Equals(SHOW_CHOICE))
                {
                    choiceWindow.SetActive(true);
                }
                else if (message.Equals(HIDE_CHOICE))
                {
                    choiceWindow.SetActive(false);
                }
            }
        }
    }
}
