using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.MessageQueue;

public class LoadingSingleton : MonoBehaviour
{

    //public static LoadingSingleton Loading_Instance = new LoadingSingleton();
    public int nextSceneToLoad = 1;

    private void OnEnable()
    {
        MessageQueuesManager.MessagePopEvent += HandleMessage;
    }

    private void OnDisable()
    {
        MessageQueuesManager.MessagePopEvent -= HandleMessage;
    }

    /// <summary>
    /// Listens for messages that tell us load a new scene
    /// </summary>
    /// <param name="id"></param>
    /// <param name="msg"></param>
    private void HandleMessage(string id, string msg)
    {
        if (id.Equals(MessageQueueID.SCENE_LOAD))
        {
            LoadTargetScene(int.Parse(msg));
        }
    }

    /// <summary>
    /// Begins loading the target scene
    /// </summary>
    /// <param name="nextScene"></param>
    private void LoadTargetScene(int nextScene)
    {
        SetNextSceneToLoad(nextScene);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Sets the target scene to load after the loading scene
    /// </summary>
    /// <param name="sceneNumber"></param>
    private void SetNextSceneToLoad(int sceneNumber)
    {
        nextSceneToLoad = sceneNumber;
    }

    /// <summary>
    /// Gives us the target to scene to load
    /// </summary>
    /// <returns></returns>
    public int GetNextSceneToLoad()
    {
        return nextSceneToLoad;
    }
}
