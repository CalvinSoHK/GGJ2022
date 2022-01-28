using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  base used: 
 *  https://answers.unity.com/questions/547513/how-do-i-detect-when-mouse-passes-over-an-object.html
 * 
 */
namespace Player
{
    public class MouseManager : MonoBehaviour
    {
        [SerializeField]
        private Camera playerCamera;

        Ray ray;
        RaycastHit hit;

        private GameObject currentHoveredObject;

        // Update is called once per frame
        void Update()
        {
            ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals("Character"))
                {
                    currentHoveredObject = hit.collider.gameObject;
                    currentHoveredObject.GetComponent<CharacterManager>().ActivateOutline(true);
                }
                else if (currentHoveredObject != null)
                {
                    currentHoveredObject.GetComponent<CharacterManager>().ActivateOutline(false);
                    currentHoveredObject = null;
                }
            }
            else if (currentHoveredObject != null)
            {
                currentHoveredObject.GetComponent<CharacterManager>().ActivateOutline(false);
                currentHoveredObject = null;
            }
        }
    }
}
