using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

namespace Exit
{
    public class ExitMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject exitCanvas;

        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!exitCanvas.activeSelf)
                {
                    exitCanvas.SetActive(true);
                }
                else
                {
                    exitCanvas.SetActive(false);
                }
            }
        }

        private void HandleMessage(string id, string msg)
        {
            if (id.Equals(MessageQueueID.GAMESTATE))
            {
                if (msg.Equals("CloseWindow"))
                {
                    exitCanvas.SetActive(false);
                }
            }
        }
    }
}
