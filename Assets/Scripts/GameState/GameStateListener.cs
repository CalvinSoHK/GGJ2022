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
                    Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                        MessageQueueID.SCENE_LOAD,
                        "2");
                }
                else if (msg.Equals("EXIT"))
                {
                    Application.Quit();
                }
            }
        }

        
    }
}
