using Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

/*
 *  base used: 
 *  https://answers.unity.com/questions/547513/how-do-i-detect-when-mouse-passes-over-an-object.html
 * 
 */
namespace Player
{
    /// <summary>
    /// Lets us investigate in the scene
    /// </summary>
    public class InvestigationManager : MonoBehaviour
    {
        [SerializeField]
        private Camera playerCamera;

        Ray ray;
        RaycastHit hit;

        private GameObject currentHoveredObject;

        /// <summary>
        /// Whether or not we can use the mouse to investigate clickable objects.
        /// </summary>
        private bool mouseActive = false;

        public static string MOUSE_ACTIVE_MESSAGE = "MouseActive";

        public static string MOUSE_INACTIVE_MESSAGE = "MouseInactive";

        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        void Update()
        {
            if (mouseActive)
            {
                ProcessMouse();
            }
        }

        /// <summary>
        /// Disables the outline and the currentHoveredObject, then sets it back to null
        /// </summary>
        private void DisableLastOutline()
        {
            currentHoveredObject.GetComponent<ClickableObject>().SetOutlineActive(false);
            currentHoveredObject = null;
        }

        /// <summary>
        /// Processes mouse position and hovers on objects and allows clicking
        /// </summary>
        private void ProcessMouse()
        {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<ClickableObject>() != null)
                {
                    currentHoveredObject = hit.collider.gameObject;
                    currentHoveredObject.GetComponent<ClickableObject>().SetOutlineActive(true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        currentHoveredObject.GetComponent<ClickableObject>().ClickObject();
                    }
                }
                else if (currentHoveredObject != null)
                {
                    DisableLastOutline();
                }
            }
            else if (currentHoveredObject != null)
            {
                DisableLastOutline();
            }
        }

        private void HandleMessage(string id, string msg)
        {
            if (id.Equals(MessageQueueID.UI))
            {
                if (msg.Equals(MOUSE_ACTIVE_MESSAGE))
                {
                    mouseActive = true;
                }
                else if (msg.Equals(MOUSE_INACTIVE_MESSAGE))
                {
                    mouseActive = false;
                }
            }
        }
    }
}
