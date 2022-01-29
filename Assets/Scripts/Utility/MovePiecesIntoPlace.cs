using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiecesIntoPlace : MonoBehaviour
{

    public List<GameObject> children = new List<GameObject>();
    public List<Vector3> childrenEndPosition = new List<Vector3>();
    public List<Vector3> childrenStartPosition = new List<Vector3>();

    public Vector2 randomizeDistance;

    // Movement speed in units per second.
    public float speed = 100.0F;

    // Time when the movement started.
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        getAllRelevantChildren();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        for(var i = 0; i < children.Count; i ++)
        {
            if(!children[i].activeSelf)
            {
                children[i].SetActive(true);
            }
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / Vector3.Distance(childrenStartPosition[i], childrenEndPosition[i]); ;

            // Set our position as a fraction of the distance between the markers.
            children[i].transform.position = Vector3.Lerp(childrenStartPosition[i], childrenEndPosition[i], fractionOfJourney);
        }
    }

    void getAllRelevantChildren()
    {
        Transform currentTransform = this.gameObject.transform;
        for(var i = 0; i < currentTransform.childCount; i++ )
        {
            Transform currentChild = currentTransform.GetChild(i);
            for (var j = 0; j < currentChild.childCount; j++)
            {
                Transform grandChild = currentChild.GetChild(j);
                children.Add(grandChild.gameObject);
                childrenEndPosition.Add(grandChild.position);
                childrenStartPosition.Add(new Vector3(grandChild.position.x + Random.Range(randomizeDistance.x, randomizeDistance.y),    //x
                    grandChild.position.y + Random.Range(randomizeDistance.x, randomizeDistance.y),                                      //y
                    grandChild.position.z + Random.Range(randomizeDistance.x, randomizeDistance.y)));                                    //z
                grandChild.gameObject.SetActive(false);
            }
        }
    }
}
