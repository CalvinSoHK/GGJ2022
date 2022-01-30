using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

public class MovePiecesIntoPlace : MonoBehaviour
{

    public List<GameObject> children = new List<GameObject>();
    public List<Vector3> childrenEndPosition = new List<Vector3>();
    public List<Vector3> childrenStartPosition = new List<Vector3>();

    public enum PieceState { Idle, MovingIn, MovingOut }; //list of the different states the moving pieces can be in
    public PieceState currentState; //keeps track of the state the pieces are currently in

    public Vector2 randomizeDistance;

    // Movement speed in units per second.
    public float speed = 100.0F;

    // Time when the movement started.
    private float startTime;

    public static string DIORAMA_COMPLETE = "DioramaComplete";

    private void OnEnable()
    {
        MessageQueuesManager.MessagePopEvent += HandleMessage;
    }

    private void OnDisable()
    {
        MessageQueuesManager.MessagePopEvent -= HandleMessage;
    }


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        getAllRelevantChildren();
        setCurrentState(PieceState.MovingIn);
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;
        var stillActive = false;

        switch(currentState)
        {
            case PieceState.Idle:

                break;

            case PieceState.MovingIn:

                for (var i = 0; i < children.Count; i++)
                {
                    if (!children[i].activeSelf)
                    {
                        children[i].SetActive(true);
                    }
                    // Fraction of journey completed equals current distance divided by total distance.
                    float fractionOfJourney = distCovered / Vector3.Distance(childrenStartPosition[i], childrenEndPosition[i]);

                    // Set our position as a fraction of the distance between the markers.
                    children[i].transform.position = Vector3.Lerp(childrenStartPosition[i], childrenEndPosition[i], fractionOfJourney);

                    if(fractionOfJourney < 1 && !stillActive)
                    {
                        stillActive = true;
                    }
                }

                if (!stillActive)
                {
                    QueueCompleteMessage();
                    setCurrentState(PieceState.Idle);
                }

                break;

            case PieceState.MovingOut:
                for (var i = 0; i < children.Count; i++)
                {
                    // Fraction of journey completed equals current distance divided by total distance.
                    float fractionOfJourney = distCovered / Vector3.Distance(childrenStartPosition[i], childrenEndPosition[i]);

                    // Set our position as a fraction of the distance between the markers.
                    children[i].transform.position = Vector3.Lerp(childrenEndPosition[i], childrenStartPosition[i], fractionOfJourney);

                    if (fractionOfJourney >= 1)
                    {
                        children[i].SetActive(false);
                    }

                    if(children[i].activeSelf && !stillActive)
                    {
                        stillActive = true;
                    }
                }

                if(!stillActive)
                {
                    QueueCompleteMessage();
                    setCurrentState(PieceState.Idle);
                }

                break;
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

    /// <summary>
    /// sets the current state of the game manager
    /// </summary>
    /// <param name="state"></param>
    public void setCurrentState(PieceState state)
    {

        startTime = 0f;
        startTime = Time.time;
        //update the state and the time since the state has been changes
        currentState = state;
        //lastStateChange = Time.time;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="msg"></param>
    private void HandleMessage(string id, string msg)
    {
        if (id.Equals(MessageQueueID.GAMESTATE))
        {
            if (msg.Equals("Choose"))
            {
                setCurrentState(PieceState.MovingOut);
            }
        }
    }

    private void QueueCompleteMessage()
    {
        Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
            MessageQueueID.GAMESTATE,
            DIORAMA_COMPLETE
            );
    }
}
