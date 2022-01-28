using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiecesIntoPlace : MonoBehaviour
{

    public List<GameObject> children = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        getAllRelevantChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getAllRelevantChildren()
    {
        Transform currentTransform = this.gameObject.transform;
        for(var i = 0; i < currentTransform.childCount; i++ )
        {
            Transform currentChild = currentTransform.GetChild(i);
            for (var j = 0; j < currentChild.childCount; j++)
            {

            }
        }
    }
}
