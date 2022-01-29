using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Utility.MessageQueue;

namespace Event
{
    /// <summary>
    /// Listens for a specific message from a specific queue and fires an event
    /// </summary>
    public class OnListenFireAfterDelayEvent : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnListenFire;

        [SerializeField]
        private float delayTime = 0f;

        [SerializeField]
        private string queueID;

        [SerializeField]
        private string message;

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
            if (queueID.Equals(id))
            {
                if (message.Equals(msg))
                {
                    FireAfterDelay();
                }
            }
        }

        private async void FireAfterDelay()
        {
            await Task.Delay((int)(delayTime * 1000));
            OnListenFire?.Invoke();
        }
    }
}
