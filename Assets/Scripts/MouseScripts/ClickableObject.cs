using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public class ClickableObject : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnClickEvent;

        [SerializeField]
        private Outline outline;

        [SerializeField]
        private float outlineWidth;

        // Start is called before the first frame update
        void Start()
        {
            SetupOutline();
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
            outline.OutlineWidth = value ? outlineWidth : 0;
        }

        public void ClickObject()
        {
            OnClickEvent?.Invoke();
        }
    }
}
