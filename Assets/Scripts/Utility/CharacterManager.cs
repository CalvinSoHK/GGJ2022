using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{

    public Outline outline;
    public float outlineWidth;
    // Start is called before the first frame update
    void Start()
    {
        SetupOutline();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupOutline()
    {
        if (outline == null)
        {
            outline = this.GetComponent<Outline>();

        }
    }

    public void ActivateOutline(bool value)
    {
        outline.OutlineWidth = value ? outlineWidth : 0;
    }
}
