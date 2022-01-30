using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLoadingText : MonoBehaviour
{
    public Text currentTextObject;
    public string[] potentialText;
    

    private bool animatingText = false; // displaying the letters in text
    private string fullText = ""; // reference to the complete string to be displayed
    private string currentText = ""; //reference that holds letters from full text
    private int currentTextIndex = 0; //index of which char from fullText to display next
    private float letterTimer = 0f; // current time between letters

    int firstScene = 3;
    public bool isMainText = false;

    [SerializeField] //amount of time between each letter
    private float letterTimerMax = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        fullText = potentialText[Random.Range(0, potentialText.Length - 1)];
        int sceneValue = Singleton.Instance.GetComponent<LoadingSingleton>().nextSceneToLoad;
        Debug.Log(sceneValue);
        if (firstScene != sceneValue || isMainText)
        {
            animatingText = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animatingText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowFullText();
            }
            else
            {
                letterTimer += Time.deltaTime;
                if (letterTimer >= letterTimerMax)
                {
                    DisplayNextLetter();
                    letterTimer = 0f;
                }
            }
        }
    }


    /// <summary>
    /// adds next letter from the message
    /// </summary>
    private void DisplayNextLetter()
    {
        if (currentTextIndex < fullText.Length)
        {
            currentText += fullText[currentTextIndex];
            currentTextObject.text = currentText;
            currentTextIndex += 1;
        }
        else
        {
            ShowFullText();
        }

    }

    /// <summary>
    /// shows the message in full and activates the next button
    /// </summary>
    private void ShowFullText()
    {
        currentTextObject.text = fullText;
        animatingText = false;
        //textNextButton.SetActive(true);
    }
}
