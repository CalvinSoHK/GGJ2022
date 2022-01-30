using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility.MessageQueue;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private Slider _progressBar;
    int key = 0;
    float timer = 0;
    bool timerGoing = true;

    [SerializeField]
    private float loadDelay;

    public static string SCENELOAD_MSG_PREFIX = "SceneLoad/";

    // Start is called before the first frame update
    void Start()
    {
        //start async operation
        LoadingSingleton loading = Singleton.Instance.GetComponent<LoadingSingleton>();
        if (loading == null)
        {
            throw new System.Exception("QueueMessageEvent Error: No LoadingSingleton on Singleton instance: " + Singleton.Instance.name);
        }

        key = loading.GetNextSceneToLoad();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGoing)
        {
            timer += Time.deltaTime;
            if (timer > loadDelay)
            {
                StartCoroutine(LoadAsyncOperation());
                timerGoing = false;
            }
        }

    }

    /// <summary>
    /// Async operation to load the scene.
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(key);
        while (gameLevel.progress < 1)
        {
            _progressBar.value = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }

        //Queue a message that scene is loaded in case we need to do stuff with it
        Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
            MessageQueueID.GAMESTATE,
            SCENELOAD_MSG_PREFIX + key);
    }
}
