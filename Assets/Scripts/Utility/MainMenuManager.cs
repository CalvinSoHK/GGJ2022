using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGameSelected(int nextScene)
    {
        LoadingSingleton loading = Singleton.Instance.GetComponent<LoadingSingleton>();
        if (loading == null)
        {
            throw new System.Exception("QueueMessageEvent Error: No LoadingSingleton on Singleton instance: " + Singleton.Instance.name);
        }

        loading.setNextSceneToLoad(nextScene);
        SceneManager.LoadScene(1);
    }
}
