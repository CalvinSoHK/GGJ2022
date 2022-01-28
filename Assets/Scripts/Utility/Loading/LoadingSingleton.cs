using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSingleton : MonoBehaviour
{

    //public static LoadingSingleton Loading_Instance = new LoadingSingleton();
    int nextSceneToLoad = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setNextSceneToLoad(int sceneNumber)
    {
        nextSceneToLoad = sceneNumber;
    }

    public int getNextSceneToLoad()
    {
        return nextSceneToLoad;
    }
}
