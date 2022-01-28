using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;


namespace Listener
{
    public class GameStateListener : MonoBehaviour
    {
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
            if (id.Equals(MessageQueueID.GAMESTATE))
            {
                if (msg.Equals("START"))
                {
                    Debug.LogWarning("Not doing anything with start yet.");
                }
                else if (msg.Equals("EXIT"))
                {
                    Application.Quit();
                }
            }
        }
    }
}
