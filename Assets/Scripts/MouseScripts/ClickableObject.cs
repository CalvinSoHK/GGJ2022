using Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility.MessageQueue;

namespace Interactable
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnClickEvent;

        [SerializeField]
        private GameObject selectionMarker;

        [SerializeField]
        private Outline outline;

        [SerializeField]
        private float outlineWidth;

        [SerializeField]
        private bool firstChara = false;

        [SerializeField]
        private QueueMessageEvent messageEvent;

        [Tooltip("Grabs random text if true")]
        [SerializeField]
        private bool grabText = true;

        [Tooltip("Whether it should grab dying text")]
        [SerializeField]
        private bool grabDeathString = false;

        // Start is called before the first frame update
        void Start()
        {
            SetupOutline();
            if(firstChara)
            {
                Singleton.Instance.GetComponent<AllPairedTextHolder>().setNewIndex();
                
            }
        }

        /// <summary>
        /// Initializes Outline component
        /// </summary>
        void SetupOutline()
        {
            if (outline == null)
            {
                outline = this.GetComponent<Outline>();
            }
        }

        /// <summary>
        /// Sets outline active state        
        /// /// </summary>
        /// <param name="value"></param>
        public void SetOutlineActive(bool value)
        {
            selectionMarker.SetActive(value);
        }

        public void ClickObject()
        {
            if(grabText)
            {
                grabText = false;
                messageEvent.AddMessage(
                    MessageQueueID.DIALOGUE, 
                    Singleton.Instance.GetComponent<AllPairedTextHolder>().returnNeededString(firstChara)
                    );
            }
            else if (grabDeathString)
            {
                grabDeathString = false;
                messageEvent.AddMessage(
                    MessageQueueID.DIALOGUE,
                    Singleton.Instance.GetComponent<AllPairedTextHolder>().GetDeathLine()
                    );
            }
            OnClickEvent?.Invoke();
        }
    }
}
