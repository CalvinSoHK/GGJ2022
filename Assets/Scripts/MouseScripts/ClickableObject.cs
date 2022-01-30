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
        private GameObject selectionMarker;

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
            OnClickEvent?.Invoke();
        }
    }
}
