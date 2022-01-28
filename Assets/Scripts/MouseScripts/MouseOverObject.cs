using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  https://answers.unity.com/questions/547513/how-do-i-detect-when-mouse-passes-over-an-object.html
 * 
 */

public class MouseOverObject : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
}
